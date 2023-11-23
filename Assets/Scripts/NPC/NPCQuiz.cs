using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Basic3rdPersonMovementAndCamera;

public class NPCQuiz : MonoBehaviour
{
    public string npcName;
    // public TextMeshProUGUI npcNameText;

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
    private QuizManager quizManager;
    private GUIManager guiManager;

    void Start()
    {
        quizManager = GameObject.FindGameObjectWithTag("QuizManager").GetComponent<QuizManager>();
        guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
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
            guiManager.interactGUI.SetActive(true);
            guiManager.interactText.text = npcName;
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
        guiManager.interactGUI.SetActive(false);
        if (isQuizComplete == false)
        {
            SetNPCDialogue(npcName);

            guiManager.dialogueGUI.SetActive(true);

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
        guiManager.interactGUI.SetActive(false);

        if (isQuizComplete == false)
        {
            guiManager.npcNameText.text = npcName;
            guiManager.interactText.text = npcName;
            Debug.Log("count" + QuizManager.answeredQuizCount);

            if (npcName == "Mr. Dela Cruz")
            {
                canTakeQuestion = true;
                guiManager.dialogueText.text = "I am Mr. Dela Cruz and I have a quiz for you";
            }
            else if (npcName == "Mr. Bautista")
            {
                canTakeQuestion = true;
                guiManager.dialogueText.text = "I am Mr. Bautista and I have a quiz for you";
            }
            else if (npcName == "Ms. Gomez")
            {
                canTakeQuestion = true;
                guiManager.dialogueText.text = "I am Ms. Gomez and I have a quiz for you";
            }
            else if (npcName == "Ms. Fernandez")
            {
                if (QuizManager.answeredQuizCount == 3)
                {
                    guiManager.dialogueText.text = "I am Ms. Fernandez and I have a quiz for you";
                    canTakeQuestion = true;
                }
                else
                {
                    canTakeQuestion = false;
                    guiManager.dialogueText.text = "You can't take this exam yet.";
                }
            }

            guiManager.dialogueGUI.SetActive(true);

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
        guiManager.interactGUI.SetActive(false);
        guiManager.dialogueGUI.SetActive(false);
        guiManager.choicesGUI.SetActive(false);
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
            guiManager.dialogueGUI.SetActive(true);
        }

        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                HandleQuizAcceptance();
                yield break;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                guiManager.dialogueText.text = "You declined the quiz";
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
            guiManager.dialogueText.text = questions[currentQuestionIndex];
            guiManager.choicesGUI.SetActive(true);
            guiManager.dialogueGUI.SetActive(true);
            SetChoicesText();
        }
        else
        {
            if (currentQuestionIndex == 3)
            {
                QuizManager.answeredQuizCount++;
                Debug.Log("count ng anwswerCount" + QuizManager.answeredQuizCount);
                guiManager.dialogueGUI.SetActive(true);
                UpdateCorrectAnswersText();
                guiManager.choicesGUI.SetActive(false);
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
            guiManager.falseChoiceText.text = "HTMPL";
            guiManager.trueChoiceText.text = "HTML";
        }
        else if (currentQuestionIndex == 1)
        {
            guiManager.falseChoiceText.text = "String";
            guiManager.trueChoiceText.text = "Character";
        }
        else if (currentQuestionIndex == 2)
        {
            guiManager.falseChoiceText.text = "Loobean";
            guiManager.trueChoiceText.text = "Boolean";
        }
    }

    void SetMrBautistaChoices()
    {
        if (currentQuestionIndex == 0)
        {
            guiManager.falseChoiceText.text = "Array";
            guiManager.trueChoiceText.text = "Set";
        }
        else if (currentQuestionIndex == 1)
        {
            guiManager.falseChoiceText.text = "Relation";
            guiManager.trueChoiceText.text = "Duo";
        }
        else if (currentQuestionIndex == 2)
        {
            guiManager.falseChoiceText.text = "Method";
            guiManager.trueChoiceText.text = "Function";
        }
    }

    void SetMsGomezChoices()
    {
        if (currentQuestionIndex == 0)
        {
            guiManager.falseChoiceText.text = "Tutorial";
            guiManager.trueChoiceText.text = "Algorithm";
        }
        else if (currentQuestionIndex == 1)
        {
            guiManager.falseChoiceText.text = "Variable";
            guiManager.trueChoiceText.text = "Things";
        }
        else if (currentQuestionIndex == 2)
        {
            guiManager.falseChoiceText.text = "Storage";
            guiManager.trueChoiceText.text = "Array";
        }
    }

    void SetMsFernandezChoices()
    {
        if (currentQuestionIndex == 0)
        {
            guiManager.falseChoiceText.text = "String";
            guiManager.trueChoiceText.text = "Character";
        }
        else if (currentQuestionIndex == 1)
        {
            guiManager.falseChoiceText.text = "Array";
            guiManager.trueChoiceText.text = "Set";
        }
        else if (currentQuestionIndex == 2)
        {
            guiManager.falseChoiceText.text = "Tutorial";
            guiManager.trueChoiceText.text = "Algorithm";
        }
    }

    void UpdateCorrectAnswersText()
    {
        interactingNPC = false;
        guiManager.dialogueText.text = "You scored " + correctAnswersCount.ToString() + " out of " + questions.Length.ToString();
    }
}