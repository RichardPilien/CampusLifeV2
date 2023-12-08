using System.Collections;
using UnityEngine;
using TMPro;

namespace Basic3rdPersonMovementAndCamera
{
    public class TutorialScript : MonoBehaviour
    {
        public string tutorialName;
        private GUIManager guiManager;
        [TextArea(6, 10)]
        public string[] tutorialDialogue;
        private int currentDialogueIndex = 0;
        private bool isTyping = false;
        private Coroutine typingCoroutine;
        private bool tutorialFinished = false; // Add this variable to track tutorial completion
        public GameObject tutorial;
        // Add a flag to track whether the deactivation coroutine is running
        private bool isDeactivationCoroutineRunning = false;

        void Start()
        {
            guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();

            guiManager.dialogueGUI.SetActive(true);
            guiManager.npcNameText.text = tutorialName;
            StartCoroutine(TypeDialogue(tutorialDialogue[currentDialogueIndex]));
        }

        void Update()
        {
            // Check for left mouse button click
            if (!isTyping && Input.GetMouseButtonDown(0))
            {
                // If not currently typing, handle the input
                HandleUserInput();
            }

            if (tutorialFinished && !isDeactivationCoroutineRunning)
            {
                // Start the coroutine to wait 4 seconds before deactivating the tutorial
                StartCoroutine(DeactivateTutorialCoroutine());
                // Start the coroutine to wait 3 seconds before deactivating the dialogue GUI
                StartCoroutine(DeactivateDialogueGUICoroutine());
            }
        }

        void HandleUserInput()
        {
            // Your existing input handling logic goes here
            if (currentDialogueIndex < tutorialDialogue.Length - 1)
            {
                // Increment the dialogue index
                currentDialogueIndex++;

                // Update the dialogue text with typing effect
                StartTypingCoroutine(tutorialDialogue[currentDialogueIndex]);
            }
            else
            {
                // If we've reached the end, deactivate the dialogue GUI
                guiManager.dialogueGUI.SetActive(false);
            }
        }

        IEnumerator DeactivateTutorialCoroutine()
        {
            isDeactivationCoroutineRunning = true;

            // Wait for 4 seconds
            yield return new WaitForSeconds(4f);

            // Deactivate the tutorial GameObject
            tutorial.SetActive(false);


            isDeactivationCoroutineRunning = false;
        }

        IEnumerator DeactivateDialogueGUICoroutine()
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Deactivate the dialogue GUI
            guiManager.dialogueGUI.SetActive(false);
        }


        IEnumerator TypeDialogue(string dialogue)
        {
            isTyping = true;
            guiManager.dialogueText.text = ""; // Clear existing text

            foreach (char letter in dialogue)
            {
                guiManager.dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f); // Adjust the delay between letters as needed
                // Debug.Log("typing? = " + isTyping);
            }

            isTyping = false;
            // Debug.Log("typing? = " + isTyping);
        }

        void StartTypingCoroutine(string dialogue)
        {
            // Stop any existing coroutine before starting a new one
            StopTypingCoroutine();
            typingCoroutine = StartCoroutine(TypeDialogue(dialogue));
        }

        void StopTypingCoroutine()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                guiManager.npcNameText.text = "Duke";
                guiManager.dialogueGUI.SetActive(true);

                // Call the TypeDialogue coroutine for the specific text
                StartTypingCoroutine("Wow, De La Salle University - Dasmariñas! This is it. Let the adventure begin!");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // guiManager.dialogueGUI.SetActive(false);
                guiManager.npcNameText.text = tutorialName;
                StartTypingCoroutine("Welcome to De La Salle University - Dasmariñas!");
                tutorialFinished = true;
            }
        }
    }
}
