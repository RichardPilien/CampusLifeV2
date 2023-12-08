using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class questManage : MonoBehaviour
    {
        public GameObject quizler;
        private NPCQuiz professorScript;
        private GUIManager guiManager;


        // Start is called before the first frame update
        void Start()
        {
            professorScript = GameObject.FindGameObjectWithTag("ProfessorScript").GetComponent<NPCQuiz>();
            guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();

            if (professorScript.quizDone == true)
            {
                // professorScript.collector.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
