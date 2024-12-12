using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4_InteractItem : MonoBehaviour
{
    public virtual void Cabinet()
    {
        if(!Level4_Manager.instance.cabinetUnlocked && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.OldParentRoom)
        {
            GameManager.instance.gameState = GameManager.GameState.Interaction;
            SettingsMenu.instance.SetVolume();
            Level4_Manager.instance.cabinetUI.SetActive(true);
            HUDController.instance.interactionText.text = string.Empty;
        }
        else if(Level4_Manager.instance.cabinetUnlocked && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.OldParentRoom && !Level4_Manager.instance.cabinetOpened)
        {
            gameObject.GetComponent<Animation>().Play();
            Level4_SoundManager.soundManager4.PlaySFX(4);
            Level4_Manager.instance.cabinetOpened = true;     
            Level4_Manager.instance.cabinetCollider.enabled = false; 
        }
        else
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && !Level4_Manager.instance.cabinetOpened )
            {
                Level4_SoundManager.soundManager4.Say(45);
            }
        }
    }

    public virtual void Certificates()
    {
        if(Level4_Manager.instance.cabinetUnlocked && !Level4_Manager.instance.cerificates)
        {
            Level4_Manager.instance.cloneObj.SetActive(false);
            Level4_Manager.instance.timerText.gameObject.SetActive(false);
            Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithUncle;
            HUDController.instance.objectiveListText.text = "Go to the dining hall and talk to your uncle.";
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level4_Manager.instance.signObjects[1].SetActive(true);
            Level4_SoundManager.soundManager4.anxietySound.Stop();
            Level4_Manager.instance.cerificates = true;
            gameObject.SetActive(false);
        }
    }

    public virtual void LoveLetters()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            Level4_SoundManager.soundManager4.Say(21);   
            Level4_Manager.instance.ShowItem(0);  
        }         
    }

    public virtual void Journal()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            Level4_SoundManager.soundManager4.Say(22);
            Level4_Manager.instance.ShowItem(1); 
        }             
    }

    public virtual void VisionBoard()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            Level4_SoundManager.soundManager4.Say(20);
        }
    }

    public virtual void InspirationalBooks()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.InspirationalBooks && !Level4_Manager.instance.inspirationalBooks)
        {
            Level4_SoundManager.soundManager4.Say(35);
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.None;
            Level4_Manager.instance.inspirationalBooks = true;

            if(Level4_Manager.instance.inspirationalBooks && Level4_Manager.instance.journal && Level4_Manager.instance.telephone && Level4_Manager.instance.drawings && Level4_Manager.instance.exercise)
            {
                HUDController.instance.objectiveListText.text = "Go talk to your Gradpa about the things that you read at his office."; 
                Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithGrandfather;
                HUDController.instance.ObjectiveUpdated.SetActive(true);
                Level4_Manager.instance.timerText.gameObject.SetActive(false);
                Level4_Manager.instance.cloneObj.SetActive(false);
                Level4_Manager.instance.signObjects[2].SetActive(true);
            }
            else 
            {
                HUDController.instance.objectiveListText.text = "Explore Grandpa's office."; 
                HUDController.instance.ObjectiveUpdated.SetActive(true);
            }
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.None && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.InspirationalBooks)
            {
                Level4_SoundManager.soundManager4.Say(46);
            }
            else if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        } 
    }

    public virtual void GrandpaJournal()
    {
         if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.Journal && !Level4_Manager.instance.journal)
        {
            Level4_SoundManager.soundManager4.Say(36);
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.None;
            Level4_Manager.instance.journal = true;

            if(Level4_Manager.instance.inspirationalBooks && Level4_Manager.instance.journal && Level4_Manager.instance.telephone && Level4_Manager.instance.drawings && Level4_Manager.instance.exercise)
            {
                HUDController.instance.objectiveListText.text = "Go talk to your Gradpa about the things that you read at his office."; 
                Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithGrandfather;
                HUDController.instance.ObjectiveUpdated.SetActive(true);
                Level4_Manager.instance.timerText.gameObject.SetActive(false);
                Level4_Manager.instance.cloneObj.SetActive(false);
                Level4_Manager.instance.signObjects[2].SetActive(true);
            }
            else 
            {
                HUDController.instance.objectiveListText.text = "Explore Grandpa's office."; 
                HUDController.instance.ObjectiveUpdated.SetActive(true);
            }
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.None && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.Journal)
            {
                Level4_SoundManager.soundManager4.Say(46);
            }
            else if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        } 
    }

    public virtual void Telephone()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.Telephone && !Level4_Manager.instance.telephone)
        {
            Level4_SoundManager.soundManager4.Say(37);
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.None;
            Level4_Manager.instance.telephone = true;

            if(Level4_Manager.instance.inspirationalBooks && Level4_Manager.instance.journal && Level4_Manager.instance.telephone && Level4_Manager.instance.drawings && Level4_Manager.instance.exercise)
            {
                HUDController.instance.objectiveListText.text = "Go talk to your Gradpa about the things that you read at his office."; 
                Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithGrandfather;
                HUDController.instance.ObjectiveUpdated.SetActive(true);
                Level4_Manager.instance.timerText.gameObject.SetActive(false);
                Level4_Manager.instance.cloneObj.SetActive(false);
                Level4_Manager.instance.signObjects[2].SetActive(true);
            }
            else 
            {
                HUDController.instance.objectiveListText.text = "Explore Grandpa's office."; 
                HUDController.instance.ObjectiveUpdated.SetActive(true);
            }
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.None && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.Telephone)
            {
                Level4_SoundManager.soundManager4.Say(46);
            }
            else if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        } 
    }   

    public virtual void Drawings()
    {
       if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.Drawings && !Level4_Manager.instance.drawings)
        {
            Level4_SoundManager.soundManager4.Say(38);
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.None;
            Level4_Manager.instance.drawings = true;

            if(Level4_Manager.instance.inspirationalBooks && Level4_Manager.instance.journal && Level4_Manager.instance.telephone && Level4_Manager.instance.drawings && Level4_Manager.instance.exercise)
            {
                HUDController.instance.objectiveListText.text = "Go talk to your Grandfather about the things that you read at his office."; 
                Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithGrandfather;
                HUDController.instance.ObjectiveUpdated.SetActive(true);
                Level4_Manager.instance.timerText.gameObject.SetActive(false);
                Level4_Manager.instance.cloneObj.SetActive(false);
                Level4_Manager.instance.signObjects[2].SetActive(true);
            }
            else 
            {
                HUDController.instance.objectiveListText.text = "Explore Grandpa's office."; 
                HUDController.instance.ObjectiveUpdated.SetActive(true);
            }
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.None && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.Drawings)
            {
                Level4_SoundManager.soundManager4.Say(46);
            }
            else if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        } 
    }

    public virtual void ExerciseEquipments()
    {
         if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.ExerciseEquipments && !Level4_Manager.instance.exercise)
        {
            Level4_SoundManager.soundManager4.Say(39); 
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.None;
            Level4_Manager.instance.exercise = true;

            if(Level4_Manager.instance.inspirationalBooks && Level4_Manager.instance.journal && Level4_Manager.instance.telephone && Level4_Manager.instance.drawings && Level4_Manager.instance.exercise)
            {
                HUDController.instance.objectiveListText.text = "Go talk to your Gradpa about the things that you read at his office."; 
                Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithGrandfather;
                HUDController.instance.ObjectiveUpdated.SetActive(true);
                Level4_Manager.instance.timerText.gameObject.SetActive(false);
                Level4_Manager.instance.cloneObj.SetActive(false);
                Level4_Manager.instance.signObjects[2].SetActive(true);
            }
            else 
            {
                HUDController.instance.objectiveListText.text = "Explore Grandpa's office."; 
                HUDController.instance.ObjectiveUpdated.SetActive(true);
            }
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying &&Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.None && Level4_Manager.instance.journalPage != Level4_Manager.JournalPage.ExerciseEquipments)
            {
                Level4_SoundManager.soundManager4.Say(46);
            }
            else if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        } 
    }

    public virtual void InspirationalBooksPage()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.None && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && !Level4_Manager.instance.inspirationalBooks)
        {
            Level4_Manager.instance.ShowItem(2);  
            HUDController.instance.objectiveListText.text = "Find Grandpa's inspirational books."; 
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.InspirationalBooks ;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        }
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        }      
    }

    public virtual void GrandpaJournalPage()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.None && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && !Level4_Manager.instance.journal)
        {
            Level4_Manager.instance.ShowItem(3);  
            HUDController.instance.objectiveListText.text = "Find Grandpa's journal.";  
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.Journal;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        }  
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        }   
    }

    public virtual void TelephonePage()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.None && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && !Level4_Manager.instance.telephone)
        {
            Level4_Manager.instance.ShowItem(4); 
            HUDController.instance.objectiveListText.text = "Find Grandpa's telephone. ";  
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.Telephone;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        }   
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        }  
    }   

    public virtual void DrawingsPage()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.None && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && !Level4_Manager.instance.drawings)
        {
            Level4_Manager.instance.ShowItem(5);
            HUDController.instance.objectiveListText.text = "Find Grandpa's drawings.";    
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.Drawings;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        } 
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        }   
    }

    public virtual void ExerciseEquipmentsPage()
    {
        if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying && Level4_Manager.instance.journalPage == Level4_Manager.JournalPage.None && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice && !Level4_Manager.instance.exercise)
        {
            Level4_Manager.instance.ShowItem(6); 
            HUDController.instance.objectiveListText.text = "Find Grandpa's exercise equipments."; 
            Level4_Manager.instance.journalPage = Level4_Manager.JournalPage.ExerciseEquipments;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        } 
        else 
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
            } 
        }      
    }
}