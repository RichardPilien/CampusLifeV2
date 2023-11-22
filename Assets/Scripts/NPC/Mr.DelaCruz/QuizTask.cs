using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Basic3rdPersonMovementAndCamera
{
    public class QuizTask : MonoBehaviour
    {
        private QuizManager quizManager;
        private PlayerInput playerInput;
        private FootStepScript footStepScript;
        public GameObject cameraNPC;
        private bool interactNPC;
        private bool talkingToNPC;
        public string nameOfNPC;
        public string[] questions;
        public int currentQuestionIndex = 0;
        public bool addQuestion = false;
        public bool isQuizComplete = false;
        public bool[] answers;
        public GameObject interactGUI;
        public TextMeshProUGUI interactText;
        public GameObject dialogueGUI;
        public TextMeshProUGUI nameOfNPCText;
        public TextMeshProUGUI dialogueText;

        // Start is called before the first frame update
        void Start()
        {
            playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
            footStepScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FootStepScript>();

            quizManager = GameObject.FindGameObjectWithTag("QuizManager").GetComponent<QuizManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactNPC && Input.GetKeyDown(KeyCode.F))
            {
                talkingToNPC = true;

                playerInput.enabled = false;
                footStepScript.enabled = false;
                cameraNPC.SetActive(false);

                dialogueGUI.SetActive(true);
                nameOfNPCText.text = nameOfNPC;
                dialogueText.text = "Hello Player, I have a quiz for you.";

                interactGUI.SetActive(false);

                currentQuestionIndex = 0;
            }

            if (talkingToNPC = true && Input.GetMouseButtonDown(0))
            {
                dialogueText.text = "question 1";
            }

            if (addQuestion == true)
            {
                quizManager.UpdateTotalQuestions(questions.Length);
                quizManager.quizCompleted = false;
                if (questions.Length == questions.Length)
                {
                    addQuestion = false;
                }
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                talkingToNPC = false;

                interactNPC = true;

                interactGUI.SetActive(true);
                interactText.text = nameOfNPC;

                dialogueGUI.SetActive(false);

            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactNPC = false;
                interactGUI.SetActive(false);
            }
        }

        void CheckAnswer(string playerAnswer)
        {

            if (isQuizComplete == false)
            {
                bool correctAnswer = answers[currentQuestionIndex];
                if (playerAnswer.ToLower() == correctAnswer.ToString().ToLower())
                {
                    Debug.Log("Correct!");
                    // correctAnswersCount++; // Increment the count of correct answers
                    quizManager.UpdateTotalCorrectAnswers(true); // Update the total correct answers in the QuizManager
                }
                else
                {
                    Debug.Log("Wrong answer.");
                    quizManager.UpdateTotalCorrectAnswers(false);
                }
            }
            currentQuestionIndex++;

            if (currentQuestionIndex >= questions.Length)
            {
                currentQuestionIndex = questions.Length;
                quizManager.CompleteQuiz();
            }

            DisplayQuestion();
        }

        void DisplayQuestion()
        {
            if (currentQuestionIndex < questions.Length)
            {
                // questionText.text = questions[currentQuestionIndex];
                // TANDF.SetActive(true);
                // QANDP.SetActive(true);
                // quizProg.SetActive(false);
            }
            else
            {
                // quizComplete.SetActive(true);
                // progress.SetActive(true);
                // UpdateCorrectAnswersText();
                // TANDF.SetActive(false);
                // QANDP.SetActive(false);
                // // playerMovement.enabled = true;
                isQuizComplete = true;
                // // You can add additional actions or rewards here when the quiz is completed.
            }
        }
    }

}
