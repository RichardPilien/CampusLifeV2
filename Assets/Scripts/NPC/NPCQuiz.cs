using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCQuiz : MonoBehaviour
{
    public string nameOfNPC;
    public TextMeshProUGUI nameOfNPCText;
    private bool interactNPC = false;
    public string[] questions;
    public bool[] answers;
    public TextMeshProUGUI fText;
    public TextMeshProUGUI tText;
    public GameObject choicesGUI;
    public GameObject dialogueGUI;
    public TextMeshProUGUI dialogueText;
    public GameObject interactGUI;
    public TextMeshProUGUI interactText;

    private bool isQuizActive = false;
    private bool hasAcceptedQuiz = false;
    private bool isQuizComplete = false;
    private bool addQuestion = false;
    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0; // Variable to store the count of correct answers

    // private PlayerMovement playerMovement;

    private QuizManager quizManager; // Reference to the QuizManager script

    //public bool isQuizNPC = false; // Flag to check if the NPC is a quiz NPC



    void Start()
    {
        // playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        quizManager = GameObject.FindGameObjectWithTag("QuizManager").GetComponent<QuizManager>();
    }

    void Update()
    {
        if (interactNPC == true)
        {
            if (isQuizActive && (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.F)))
            {
                string playerAnswer = Input.GetKeyDown(KeyCode.T) ? "True" : "False";
                CheckAnswer(playerAnswer);
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

            if (interactNPC && Input.GetKeyDown(KeyCode.F))
            {
                interactGUI.SetActive(false);
                if (isQuizComplete == false)
                {
                    nameOfNPCText.text = nameOfNPC;
                    interactText.text = nameOfNPC;

                    if (nameOfNPC == "Mr. Dela Cruz")
                    {
                        dialogueText.text = "I am Mr. Dela Cruz and I have a quiz for you";

                        tText.text = questions[currentQuestionIndex];
                    }
                    else if (nameOfNPC == "Mr. Bautista")
                    {
                        dialogueText.text = "I am Mr. Bautista and I have a quiz for you";
                    }
                    else if (nameOfNPC == "Ms. Gomez")
                    {
                        dialogueText.text = "I am Ms. Gomez and I have a quiz for you";
                    }
                    else if (nameOfNPC == "Ms. Fernandez")
                    {
                        dialogueText.text = "I am Ms. Fernandez and I have a quiz for you";
                    }

                    dialogueGUI.SetActive(true);

                    if (hasAcceptedQuiz)
                    {
                        isQuizActive = true;
                        DisplayQuestion();
                    }
                    else
                    {
                        StartCoroutine(ProcessPlayerInput());
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactNPC = true;

            interactGUI.SetActive(true);
            interactText.text = nameOfNPC;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //isQuizNPC = false;
            interactNPC = false;
            isQuizActive = false;
            hasAcceptedQuiz = false;
            currentQuestionIndex = 0;
            // playerMovement.enabled = true;
            interactGUI.SetActive(false);
            dialogueGUI.SetActive(false);
        }
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            dialogueText.text = questions[currentQuestionIndex];
            choicesGUI.SetActive(true);
            dialogueGUI.SetActive(true);
        }
        else
        {
            dialogueText.text = "Quiz Complete";
            dialogueGUI.SetActive(true);
            UpdateCorrectAnswersText();
            choicesGUI.SetActive(false);
            // playerMovement.enabled = true;
            isQuizComplete = true;
            // You can add additional actions or rewards here when the quiz is completed.
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
                correctAnswersCount++; // Increment the count of correct answers
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

    IEnumerator ProcessPlayerInput()
    {
        if (isQuizComplete == false)
        {
            // dialogueText.SetActive(true);
        }

        if (isQuizComplete == true)
        {
            // dialogueText.SetActive(false);
            dialogueGUI.SetActive(true);
        }



        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Y))
            {
                addQuestion = true;
                hasAcceptedQuiz = true;
                isQuizActive = true;
                // playerMovement.enabled = false;
                DisplayQuestion();
                yield break;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                dialogueText.text = "You declined the quiz";
                Debug.Log("You declined the quiz.");
                yield break;
            }
        }
    }

    void UpdateCorrectAnswersText()
    {
        dialogueText.text = "You scored " + correctAnswersCount.ToString() + " out of " + questions.Length.ToString();
    }
}
