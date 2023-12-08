using UnityEngine;
using System.Collections;
using TMPro;


using Basic3rdPersonMovementAndCamera;

public class DialogueManager : MonoBehaviour
{
    public string npcName;
    // [TextArea(6, 10)]
    // public string entryDialogue;
    [TextArea(6, 10)]
    public string[] dialogueLines;
    private int currentLine = 0;
    private bool playerInRange;
    private bool talking;
    private bool finishedTalking;

    private GUIManager guiManager;
    private NPCControllerMatthew npcControllerMatthew;

    private bool typing;


    void Start()
    {
        // Initialize any necessary setup here
        npcControllerMatthew = GameObject.FindGameObjectWithTag("NPCController").GetComponent<NPCControllerMatthew>();
        guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
    }

    void Update()
    {
        // Check for player input to trigger dialogue
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // Display the next line of dialogue
            talking = true;
            guiManager.dialogueGUI.SetActive(true);
            StartCoroutine(TypeDialogue(dialogueLines[currentLine]));
        }

        // Check for mouse input only if not currently typing
        if (talking && !typing && Input.GetMouseButtonDown(0))
        {
            // If currently typing, finish typing instantly
            if (typing)
            {
                StopAllCoroutines();
                guiManager.dialogueText.text = dialogueLines[currentLine];
                typing = false;
            }
            else if (currentLine < dialogueLines.Length)
            {
                // Display the next line
                DisplayNextLine();
            }
            else
            {
                // End of dialogue, you might want to reset the state or do something else
                talking = false;
                guiManager.dialogueGUI.SetActive(false);
                npcControllerMatthew.enabled = true;
                // npcControllerMatthew.speed = 2.5f;
                // npcControllerMatthew.rotationSpeed = 5f;
                // npcControllerMatthew.delayTime = 0.5f;
            }
        }
    }

    IEnumerator TypeDialogue(string line)
    {
        typing = true;
        guiManager.dialogueText.text = "";

        float delay = 0.01f; // Adjust this value to control the typing speed

        foreach (char letter in line.ToCharArray())
        {
            guiManager.dialogueText.text += letter;
            yield return new WaitForSeconds(delay); // Wait for the specified delay
        }

        typing = false;

        // Increment currentLine here, so it only happens when typing is complete
        currentLine++;
    }


    void DisplayNextLine()
    {
        // Display the current line of dialogue using TextMeshPro
        StartCoroutine(TypeDialogue(dialogueLines[currentLine]));
    }





    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            guiManager.interactText.text = npcName;
            guiManager.npcNameText.text = npcName;
            guiManager.interactGUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            guiManager.dialogueGUI.SetActive(false);
            guiManager.interactGUI.SetActive(false);

            // Reset the current line when the player exits the trigger
            currentLine = 0;
        }
    }
}
