using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class PlayerCollector : MonoBehaviour
    {
        public CollectTaskGiver[] taskGiver;
        public CollectTaskGiver taskToGive;
        private bool canCollect = false;
        private int itemIndex = -1; // Initialize with an invalid index.
        private string interactName;
        private GUIManager guiManager;
        void Update()
        {
            guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();

            if (canCollect && Input.GetKeyDown(KeyCode.F))
            {
                CollectBook();
            }
        }

        private void CollectBook()
        {
            // Loop through each element of the taskGiver array
            foreach (CollectTaskGiver giver in taskGiver)
            {
                if (giver.questStarted == true && itemIndex != -1)
                {
                    // Perform book collection logic here
                    // For example, update quest progress or inventory
                    giver.CollectBook(itemIndex); // Pass the item index to the taskGiver.                
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sphere"))
            {
                canCollect = true;

                // Loop through each element of the taskGiver array
                foreach (CollectTaskGiver giver in taskGiver)
                {
                    if (giver.questStarted == true)
                    {
                        guiManager.interactGUI.SetActive(true);
                        // taskToGive.interactText.text = giver.nameOfCollectible;

                    }
                }

                if (guiManager.interactText != null)
                {
                    guiManager.interactText.text = "To Collect.";
                }

                // Loop through each element of the taskGiver array
                foreach (CollectTaskGiver giver in taskGiver)
                {
                    // Find the index of the item in the array.
                    for (int i = 0; i < giver.itemToCollect.Length; i++)
                    {
                        if (giver.itemToCollect[i] == other.gameObject)
                        {
                            itemIndex = i;
                            break; // Exit the loop when the item is found.
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Sphere"))
            {
                canCollect = false;

                // Loop through each element of the taskGiver array
                foreach (CollectTaskGiver giver in taskGiver)
                {
                    guiManager.interactGUI.SetActive(false);
                }
                itemIndex = -1; // Reset the itemIndex when the player leaves the sphere.
            }
        }
    }
}
