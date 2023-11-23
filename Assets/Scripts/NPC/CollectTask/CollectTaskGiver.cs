using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Basic3rdPersonMovementAndCamera
{
    public class CollectTaskGiver : MonoBehaviour
    {
        public string NPCName;
        public string locationNPC;
        public string courseTitle;
        public string year;
        public string questName;
        [TextArea(5, 10)]
        public string questDescription;
        public bool taskComplete = false;
        public string nameOfCollectible;
        public GameObject[] itemToCollect;
        public int numberOfItemCollected;
        public int numberOfItemToCollect;
        public bool questStarted = false;
        private bool interactedNPC = false;
        private bool isTaskInfoVisible = false;
        private bool isProgressVisible = true;

        private GUIManager guiManager;

        public void Update()
        {
            guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
            if (interactedNPC && Input.GetKeyDown(KeyCode.F))
            {
                if (!questStarted && taskComplete == false)
                {
                    GiveQuest();
                }

                if (taskComplete == true)
                {
                    guiManager.dialogueText.text = "Here is your Reward!";
                    guiManager.taskInfoGUI.SetActive(false);
                    guiManager.progressGUI.SetActive(false);
                }
            }

            if (questStarted == true && Input.GetKeyDown(KeyCode.F1))
            {
                // Toggle the visibility state
                isTaskInfoVisible = !isTaskInfoVisible;
                isProgressVisible = !isProgressVisible;

                // Set the visibility of TaskInfoGUI based on the boolean variable
                guiManager.taskInfoGUI.SetActive(isTaskInfoVisible);
                guiManager.progressGUI.SetActive(isProgressVisible);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactedNPC = true;

                if (guiManager.interactGUI == null)
                {
                    Debug.Log("Null");
                }
                guiManager.interactGUI.SetActive(true);
                // taskToGive.DialogueGUI.SetActive(true);
                if (guiManager.interactText != null)
                {
                    guiManager.interactText.text = NPCName;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactedNPC = false;
                guiManager.interactGUI.SetActive(false);
                guiManager.dialogueGUI.SetActive(false);
            }
        }
        public void CollectBook(int itemIndex)
        {
            if (questStarted)
            {
                // Check if the itemIndex is valid.
                if (itemIndex >= 0 && itemIndex < itemToCollect.Length)
                {
                    GameObject item = itemToCollect[itemIndex];

                    // Check if the item is not null (it hasn't been collected already).
                    if (item != null)
                    {
                        Debug.Log("Collected");
                        guiManager.interactGUI.SetActive(false);
                        numberOfItemCollected++;

                        // Collect the item and destroy it.
                        Destroy(item);

                        // Optionally, remove the item from the array or set it to null.
                        itemToCollect[itemIndex] = null;
                        if (numberOfItemCollected >= numberOfItemToCollect)
                        {
                            taskComplete = true;
                            questStarted = false;
                            UpdateTaskText();
                        }
                        else
                        {
                            UpdateTaskText();
                        }
                    }
                }
            }
        }

        public void GiveQuest()
        {
            questStarted = true;

            guiManager.dialogueGUI.SetActive(true);

            guiManager.npcNameText.text = NPCName;
            guiManager.yearText.text = year;
            guiManager.courseTitleText.text = courseTitle;

            guiManager.taskInfoTitle.text = questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
            guiManager.taskInfoTitleF1.text = questName;
            guiManager.location.text = locationNPC;

            guiManager.dialogueText.text = questDescription;
            guiManager.taskInfoDescription.text = questDescription;

            guiManager.progressText.text = questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
            guiManager.progressGUI.SetActive(true);

            Debug.Log("Quest Started! " + questName);
        }

        public void UpdateTaskText()
        {
            if (questStarted == true)
            {
                guiManager.progressGUI.SetActive(true);
                guiManager.progressText.text = questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";

                guiManager.taskInfoTitle.text = questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
                guiManager.taskInfoTitleF1.text = questName;
            }
            else
            {
                guiManager.progressText.text = questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";

                guiManager.dialogueGUI.SetActive(true);
                guiManager.dialogueText.text = "Task Complete! Go back to NPC";
            }
        }
    }
}
