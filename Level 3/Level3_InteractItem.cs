using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_InteractItem : MonoBehaviour
{
    public virtual void PS4Game()
    {
        if(gameObject.name =="PS4 Game" && !Level3_Manager.instance.ps4GameCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.ps4GameCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Blue Game");
            }
            
        }
        else if(gameObject.name =="PS4 Game" && Level3_Manager.instance.ps4GameCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);
            }
        }
        else
        {
            if(Level3_Manager.instance.ps4GameCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }         
        }
    }

    public virtual void XboxGame()
    {
        if(gameObject.name =="Xbox Game" && !Level3_Manager.instance.xboxGameCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.xboxGameCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Green Game");
            }
        }
        else if(gameObject.name =="Xbox Game" && Level3_Manager.instance.xboxGameCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);
            }
        }
        else
        {
            if(Level3_Manager.instance.xboxGameCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }        
        }
    }

    public virtual void TShirt()
    {
        if(gameObject.name =="Green T-Shirt" && !Level3_Manager.instance.greenTshirtCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.greenTshirtCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Green T-Shirt");
            }
        }
        else if(gameObject.name =="Green T-Shirt" && Level3_Manager.instance.greenTshirtCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);
            }
        }
        else
        {
            if(Level3_Manager.instance.greenTshirtCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }         
        }
    }

    public virtual void Pants()
    {
        if(gameObject.name =="Gray Pants" && !Level3_Manager.instance.grayPantsCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.grayPantsCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Gray Pants");
            }
        }
        else if(gameObject.name =="Gray Pants" && Level3_Manager.instance.grayPantsCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);
            }
        }
        else
        {
            if(Level3_Manager.instance.grayPantsCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }         
        }
    }

    public virtual void Watercolor()
    {
        if(gameObject.name =="Watercolor" && !Level3_Manager.instance.watercolorCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.watercolorCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Watercolor");
            }
        }
        else if(gameObject.name =="Watercolor" && Level3_Manager.instance.watercolorCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);
            }
        }
        else
        {
            if(Level3_Manager.instance.watercolorCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }         
        }
    }

    public virtual void Notebook()
    {
        if(gameObject.name =="Red Notebook" && !Level3_Manager.instance.redNotebookCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(22);
            ItemPick();
            Level3_Manager.instance.redNotebookCollected = true;

            if(!Level3_Manager.instance.allItemsCollected)
            {
                Level3_Manager.instance.CheckAndRemoveObjective("Red Notebook");
            }
        }
        else if(gameObject.name =="Red Notebook" && Level3_Manager.instance.redNotebookCollected)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(19);

            }
        }
        else
        {
            if(Level3_Manager.instance.redNotebookCollected && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(19);
            }
            else if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            { 
                Level3_SoundManager.soundManager3.Say(12);
            }          
        }
    }

    public virtual void Cashier()
    {
        if(gameObject.name =="Cashier Desk" && Level3_Manager.instance.objectiveList == Level3_Manager.ObjectiveList.Checkout && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.PlaySFX(1);
            Level3_SoundManager.soundManager3.Say(14);
            Level3_Manager.instance.objectiveList = Level3_Manager.ObjectiveList.End;
            HUDController.instance.objectiveListText.text = "Leave through the main entrance.";
            Level3_Manager.instance.signObjects[0].SetActive(false);
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            
        }
        else if(gameObject.name =="Cashier Desk" && Level3_Manager.instance.objectiveList == Level3_Manager.ObjectiveList.End)
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {            
                Level3_SoundManager.soundManager3.Say(21);
            }        
        }
        else
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(20);
            }
        }
    }

    private void ItemPick()
    {
        Level3_Manager.instance.itemsCollected++;

        if(Level3_Manager.instance.itemsCollected >= 5 && !Level3_Manager.instance.allItemsCollected)
        {
            Level3_Manager.instance.timerText.gameObject.SetActive(false);
            HUDController.instance.objectiveListText.text = "Proceed to the cashier to pay for the items.";
            Level3_Manager.instance.objectiveList = Level3_Manager.ObjectiveList.Checkout;
            Level3_Manager.instance.allItemsCollected = true;
            Level3_Manager.instance.signObjects[0].SetActive(true);
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        }
    }
}
