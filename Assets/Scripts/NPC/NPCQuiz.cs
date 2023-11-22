using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Basic3rdPersonMovementAndCamera;

public class NPCQuiz : MonoBehaviour
{
    public string npcName;
    public TextMeshProUGUI npcNameText;

    private bool interactingNPC = false;
    private bool isQuizActive = false;
    private bool hasAcceptedQuiz = false;
    private bool isQuizComplete = false;
    private bool addQuestion = false;
    private bool canTakeQuestion = false;
    private int currentQuestionIndex = 0;
    private int correctAnswersCount = 0;

    [TextArea(6, 10)]
    public string[] questions;
    public bool[] answers;

    public TextMeshProUGUI falseChoiceText;
    public TextMeshProUGUI trueChoiceText;
    // public TextMeshProUGUI quizResultText;

    public GameObject choicesGUI;
    public GameObject dialogueGUI;
    public TextMeshProUGUI dialogueText;
    public GameObject interactGUI;
    public TextMeshProUGUI interactText;

    private QuizManager quizManager;

    void Start()
    {
        quizManager = GameObject.FindGameObjectWithTag("QuizManager").GetComponent<QuizManager>();
    }

    void Update()
    {
        if (interactingNPC)
        {
            HandleQuizInput();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactingNPC = true;
            interactGUI.SetActive(true);
            interactText.text = npcName;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetQuizState();
        }
    }

    void HandleQuizInput()
    {
        if (isQuizActive && (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.F)))
        {
            string playerAnswer = Input.GetKeyDown(KeyCode.T) ? "True" : "False";
            CheckAnswer(playerAnswer);
        }

        if (addQuestion)
        {
            UpdateQuizManager();
        }

        if (interactingNPC && Input.GetKeyDown(KeyCode.F))
        {
            StartQuizDialogue();
        }
    }

    void CheckAnswer(string playerAnswer)
    {
        if (isQuizComplete) return;

        bool correctAnswer = answers[currentQuestionIndex];
        if (playerAnswer.ToLower() == correctAnswer.ToString().ToLower())
        {
            Debug.Log("Correct!");
            correctAnswersCount++;
            quizManager.UpdateTotalCorrectAnswers(true);
        }
        else
        {
            Debug.Log("Wrong answer.");
            quizManager.UpdateTotalCorrectAnswers(false);
        }

        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Length)
        {
            currentQuestionIndex = questions.Length;
            quizManager.CompleteQuiz();
        }

        DisplayQuestion();
    }

    void UpdateQuizManager()
    {
        quizManager.UpdateTotalQuestions(questions.Length);
        quizManager.quizCompleted = false;
        if (questions.Length == questions.Length)
        {
            addQuestion = false;
        }
    }

    void StartQuizDialogue()
    {
        interactGUI.SetActive(false);
        if (isQuizComplete == false)
        {
            SetNPCDialogue(npcName);

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

    void SetNPCDialogue(string npcName)
    {
        interactGUI.SetActive(false);

        if (isQuizComplete == false)
        {
            npcNameText.text = npcName;
            interactText.text = npcName;
            Debug.Log("count" + QuizManager.answeredQuizCount);

            if (npcName == "Mr. Dela Cruz")
            {
                canTakeQuestion = true;
                dialogueText.text = "I am Mr. Dela Cruz and I have a quiz for you";
            }
            else if (npcName == "Mr. Bautista")
            {
                canTakeQuestion = true;
                dialogueText.text = "I am Mr. Bautista and I have a quiz for you";
            }
            else if (npcName == "Ms. Gomez")
            {
                canTakeQuestion = true;
                dialogueText.text = "I am Ms. Gomez and I have a quiz for you";
            }
            else if (npcName == "Ms. Fernandez")
            {
                if (QuizManager.answeredQuizCount == 3)
                {
                    dialogueText.text = "I am Ms. Fernandez and I have a quiz for you";
                    canTakeQuestion = true;
                }
                else
                {
                    canTakeQuestion = false;
                    dialogueText.text = "You can't take this exam yet.";
                }
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


    void ResetQuizState()
    {
        interactingNPC = false;
        isQuizActive = false;
        hasAcceptedQuiz = false;
        currentQuestionIndex = 0;
        interactGUI.SetActive(false);
        dialogueGUI.SetActive(false);
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
                HandleQuizAcceptance();
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

    void HandleQuizAcceptance()
    {
        Debug.Log("pindot Y");
        if (canTakeQuestion)
        {
            addQuestion = true;
            hasAcceptedQuiz = true;
            isQuizActive = true;
            DisplayQuestion();
        }
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            dialogueText.text = questions[currentQuestionIndex];
            choicesGUI.SetActive(true);
            dialogueGUI.SetActive(true);
            SetChoicesText();
        }
        else
        {
            if (currentQuestionIndex == 3)
            {
                QuizManager.answeredQuizCount++;
                Debug.Log("count ng anwswerCount" + QuizManager.answeredQuizCount);
                dialogueGUI.SetActive(true);
                UpdateCorrectAnswersText();
                choicesGUI.SetActive(false);
                isQuizComplete = true;
            }
        }
    }

    void SetChoicesText()
    {
        if (npcName == "Mr. Dela Cruz")
        {
            SetMrDelaCruzChoices();
        }
        else if (npcName == "Mr. Bautista")
        {
            SetMrBautistaChoices();
        }
        else if (npcName == "Ms. Gomez")
        {
            SetMsGomezChoices();
        }
        else if (npcName == "Ms. Fernandez")
        {
            SetMsFernandezChoices();
        }
    }

    void SetMrDelaCruzChoices()
    {
        if (currentQuestionIndex == 0)
        {
            falseChoiceText.text = "HTMPL";
            trueChoiceText.text = "HTML";
        }
        else if (currentQuestionIndex == 1)
        {
            falseChoiceText.text = "String";
            trueChoiceText.text = "Character";
        }
        else if (currentQuestionIndex == 2)
        {
            falseChoiceText.text = "Loobean";
            trueChoiceText.text = "Boolean";
        }
    }

    void SetMrBautistaChoices()
    {
        if (currentQuestionIndex == 0)
        {
            falseChoiceText.text = "Array";
            trueChoiceText.text = "Set";
        }
        else if (currentQuestionIndex == 1)
        {
            falseChoiceText.text = "Relation";
            trueChoiceText.text = "Duo";
        }
        else if (currentQuestionIndex == 2)
        {
            falseChoiceText.text = "Method";
            trueChoiceText.text = "Function";
        }
    }

    void SetMsGomezChoices()
    {
        if (currentQuestionIndex == 0)
        {
            falseChoiceText.text = "Tutorial";
            trueChoiceText.text = "Algorithm";
        }
        else if (currentQuestionIndex == 1)
        {
            falseChoiceText.text = "Variable";
            trueChoiceText.text = "Things";
        }
        else if (currentQuestionIndex == 2)
        {
            falseChoiceText.text = "Storage";
            trueChoiceText.text = "Array";
        }
    }

    void SetMsFernandezChoices()
    {
        if (currentQuestionIndex == 0)
        {
            falseChoiceText.text = "String";
            trueChoiceText.text = "Character";
        }
        else if (currentQuestionIndex == 1)
        {
            falseChoiceText.text = "Array";
            trueChoiceText.text = "Set";
        }
        else if (currentQuestionIndex == 2)
        {
            falseChoiceText.text = "Tutorial";
            trueChoiceText.text = "Algorithm";
        }
    }

    void UpdateCorrectAnswersText()
    {
        interactingNPC = false;
        dialogueText.text = "You scored " + correctAnswersCount.ToString() + " out of " + questions.Length.ToString();
    }
}