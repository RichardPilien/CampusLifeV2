using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class CollectTaskGiver : MonoBehaviour
    {
        public NPCCollectTask taskToGive;
        public string nameOfCollectible;
        public GameObject[] itemToCollect;
        public int numberOfItemCollected;
        public int numberOfItemToCollect;
        public bool questStarted = false;
        private bool interactedNPC = false;
        private bool isTaskInfoVisible = false;
        private bool isProgressVisible = true;

        public void Update()
        {
            // taskToGive.InteractGUI = GameObject.Find("InteractGUI");
            if (interactedNPC && Input.GetKeyDown(KeyCode.F))
            {
                if (!questStarted && taskToGive.taskComplete == false)
                {
                    GiveQuest();
                }

                if (taskToGive.taskComplete == true)
                {
                    taskToGive.dialogueText.text = "Here is your Reward!";
                    taskToGive.TaskInfoGUI.SetActive(false);
                    taskToGive.ProgressGUI.SetActive(false);
                }
            }

            if (questStarted == true && Input.GetKeyDown(KeyCode.F1))
            {
                // Toggle the visibility state
                isTaskInfoVisible = !isTaskInfoVisible;
                isProgressVisible = !isProgressVisible;

                // Set the visibility of TaskInfoGUI based on the boolean variable
                taskToGive.TaskInfoGUI.SetActive(isTaskInfoVisible);
                taskToGive.ProgressGUI.SetActive(isProgressVisible);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactedNPC = true;

                if (taskToGive.InteractGUI == null)
                {
                    Debug.Log("Null");
                }
                taskToGive.InteractGUI.SetActive(true);
                // taskToGive.DialogueGUI.SetActive(true);
                if (taskToGive.interactText != null)
                {
                    taskToGive.interactText.text = taskToGive.NPCName;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactedNPC = false;
                taskToGive.InteractGUI.SetActive(false);
                taskToGive.DialogueGUI.SetActive(false);
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
                        taskToGive.InteractGUI.SetActive(false);
                        numberOfItemCollected++;

                        // Collect the item and destroy it.
                        Destroy(item);

                        // Optionally, remove the item from the array or set it to null.
                        itemToCollect[itemIndex] = null;
                        if (numberOfItemCollected >= numberOfItemToCollect)
                        {
                            taskToGive.taskComplete = true;
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

            taskToGive.DialogueGUI.SetActive(true);

            taskToGive.courseTitleText.text = taskToGive.courseTitle;
            taskToGive.yearText.text = taskToGive.year;
            taskToGive.nameText.text = taskToGive.NPCName;

            taskToGive.taskInfoTitle.text = taskToGive.questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
            taskToGive.taskInfoTitleF1.text = taskToGive.questName;
            taskToGive.location.text = taskToGive.locationNPC;

            taskToGive.dialogueText.text = taskToGive.questDescription;
            taskToGive.taskInfoDescription.text = taskToGive.questDescription;

            taskToGive.progressText.text = taskToGive.questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
            taskToGive.ProgressGUI.SetActive(true);

            Debug.Log("Quest Started! " + taskToGive.questName);
        }

        public void UpdateTaskText()
        {
            if (questStarted == true)
            {
                taskToGive.ProgressGUI.SetActive(true);
                taskToGive.progressText.text = taskToGive.questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";

                taskToGive.taskInfoTitle.text = taskToGive.questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";
                taskToGive.taskInfoTitleF1.text = taskToGive.questName;
            }
            else
            {
                taskToGive.progressText.text = taskToGive.questName + " (" + numberOfItemCollected + "/" + numberOfItemToCollect + ")";

                taskToGive.DialogueGUI.SetActive(true);
                taskToGive.dialogueText.text = "Task Complete! Go back to NPC";
            }
        }
    }
}
