using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private CharacterController controller;
    private Animator animator;
    private Camera playerCamera;

    [Header("Attributes")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    private float runCooldownTimer = 5f;
    private bool runCooldown;

    [Header("Camera")]
    public float lookSpeed = 1f;
    private const float lookXLimit = 45f;  
    private float rotationX = 0;

    [Header("Sounds")]
    public AudioSource footstepAudio;
    public AudioSource jumpAudio;
    public AudioSource breathAudio;

    [Header("General Conditions")]
    public bool canMove = true;
    private PlayerState playerState;  
    private Vector3 moveDirection  = Vector3.zero;

    [Header("Jump Components")]
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    private bool isGrounded;
    private bool jumpPressed;
    private bool canJump = true;

    [Header("Interaction Components")]
    public float playerReach;
    private Interactable currentInteractable;
    private KeyCode interactKey = KeyCode.F;

    protected virtual void Awake()
    {
        instance = this;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
    }

    protected virtual void Start()
    {
        playerState = PlayerState.Idle;
    }

    protected virtual void Update()
    {
        UpdateAnimations();
        
        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f;
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
            controller.Move(moveDirection  * Time.deltaTime); 
        }

        CheckInteractionFunction(); 
        
        if(!CanMoveInCurrentState()) return;

        if(jumpPressed)
        {
            canJump = false;
        }
        else
        {
            canJump = true;      
        } 

        PlayerMovement();
        UpdateSounds();              
    }

    private void UpdateSounds()
    {
        if (!runCooldown && playerState == PlayerState.Running)
        {
            runCooldownTimer -= Time.deltaTime;   

            if(runCooldownTimer <= 0f)
            {
                runCooldown = true;
                runCooldownTimer = 0f;
            }      
        }
        else if (runCooldown)
        {
            StartCoroutine(BreathHandler());
        }
        else
        {
            runCooldownTimer += Time.deltaTime; 

            if(runCooldownTimer >= 5f)
            {
                runCooldownTimer = 5f;
            }             
        }
    }

    private bool CanMoveInCurrentState()
    {
        return canMove &&
            (GameManager.instance.gameState == GameManager.GameState.Gameplay);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("Idle", playerState == PlayerState.Idle);
        animator.SetBool("Walking", playerState == PlayerState.Walking);
        animator.SetBool("Running", playerState == PlayerState.Running);
        animator.SetBool("Jumping", playerState == PlayerState.Jumping);
    }

    private void PlayerMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * horizontal + transform.forward * vertical).normalized;

        if (move.magnitude > 0)
        {           
            playerState = !runCooldown && Input.GetKey(KeyCode.LeftShift) ? PlayerState.Running : PlayerState.Walking;
            move *= (playerState == PlayerState.Running) ? runSpeed : walkSpeed;

            if (isGrounded)
            {
                footstepAudio.enabled = true;
                footstepAudio.pitch = (playerState == PlayerState.Running) ? 1.25f : 1f;
            }
        }
        else
        {
            playerState = PlayerState.Idle;
            footstepAudio.enabled = false;
        }

        jumpPressed = Input.GetKey(KeyCode.Space);  

        if (jumpPressed && isGrounded && canJump)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpAudio.enabled = true;
        }    
        else if (!isGrounded)
        {
            playerState = PlayerState.Jumping;
            footstepAudio.enabled = false;
        }
        else
        {
            jumpAudio.enabled = false;
        }

        controller.Move(move * Time.deltaTime);       
        HandleCameraRotation();
    }

    private IEnumerator BreathHandler()
    {
        breathAudio.enabled = true;

        yield return new WaitForSeconds(5f);
   
        runCooldown = false;
        breathAudio.enabled = false;
        runCooldownTimer = 5f;
    }

    private void HandleCameraRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void CheckInteractionFunction()
    {
        CheckInteraction();

        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interaction();
        }
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(ray, out hit, playerReach) && CanMoveInCurrentState())
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
    
    private enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
    }
}