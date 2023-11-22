using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Basic3rdPersonMovementAndCamera
{

    [System.Serializable]
    public class NPCCollectTask
    {
        public string NPCName;
        public string locationNPC;
        public string courseTitle;
        public string year;
        public string questName;
        public TextMeshProUGUI nameText;
        [TextArea(5, 10)]
        public string questDescription;
        public bool taskComplete = false;
        public GameObject InteractGUI;
        public TextMeshProUGUI interactText;         // Reference to the UI text element
        public GameObject DialogueGUI;
        public TextMeshProUGUI dialogueText;
        public GameObject ProgressGUI;
        public TextMeshProUGUI progressText;
        public GameObject TaskInfoGUI;
        public TextMeshProUGUI courseTitleText;
        public TextMeshProUGUI yearText;
        public TextMeshProUGUI taskInfoTitle;
        public TextMeshProUGUI taskInfoTitleF1;
        public TextMeshProUGUI location;
        public TextMeshProUGUI taskInfoDescription;
        // public void AddObjective(CollectibleItem item)
        // {
        //     objectives.Add(item);
        // }

        // public void Complete()
        // {
        //     taskComplete = true;
        //     dialogueText.text = "Here is your Reward!";
        //     // QuestManager.instance.CompleteQuest(this);
        // }
        public void Start()
        {
            // InteractGUI = GetComponent<GameObject>();
        }
    }
}
