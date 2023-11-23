using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Basic3rdPersonMovementAndCamera
{
    public class GUIManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public TextMeshProUGUI npcNameText;
        public GameObject dialogueGUI;
        public TextMeshProUGUI dialogueText;


        public GameObject interactGUI;
        public TextMeshProUGUI interactText;         // Reference to the UI text element


        public GameObject progressGUI;
        public TextMeshProUGUI progressText;


        public GameObject taskInfoGUI;
        public TextMeshProUGUI courseTitleText;
        public TextMeshProUGUI yearText;
        public TextMeshProUGUI taskInfoTitle;
        public TextMeshProUGUI taskInfoTitleF1;
        public TextMeshProUGUI location;
        public TextMeshProUGUI taskInfoDescription;


        public GameObject choicesGUI;
        public TextMeshProUGUI falseChoiceText;
        public TextMeshProUGUI trueChoiceText;
        // public TextMeshProUGUI quizResultText;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
