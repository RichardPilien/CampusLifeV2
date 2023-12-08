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
    private bool quizFinished = false;
    [TextArea(6, 10)]
    public string[] questions;
    public string[] answers;
    public GameObject professorScript;
    public GameObject collect;
    private QuizManager quizManager;
    private GUIManager guiManager;
    private bool isTyping = false; // Flag to check if the typing coroutine is already running
    public bool quizDone = false;
    private bool isDeactivationCoroutineRunning = false;
    // public GameObject collector;


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

        if (quizDone && !isDeactivationCoroutineRunning)
        {
            // Start the coroutine to wait 4 seconds before deactivating the tutorial
            StartCoroutine(DeactivateQuizCoroutine());
            // Start the coroutine to wait 3 seconds before deactivating the dialogue GUI
            // StartCoroutine(DeactivateDialogueGUICoroutine());
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
        if (!isTyping && isQuizActive && (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.F)))
        {
            string playerAnswer = Input.GetKeyDown(KeyCode.T) ? guiManager.trueChoiceText.text : guiManager.falseChoiceText.text;
            CheckAnswer(playerAnswer);
            Debug.Log("this is the player answer: " + playerAnswer);
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

        string correctAnswer = answers[currentQuestionIndex].ToString(); // Convert the boolean to a string

        if (playerAnswer.ToLower() == correctAnswer.ToLower())
        {
            // Debug.Log("Correct!");
            // Debug.Log("This is the correct answer: " + correctAnswer);
            correctAnswersCount++;
            quizManager.UpdateTotalCorrectAnswers(true);
        }
        else
        {
            // Debug.Log("Wrong answer.");
            // Debug.Log("This is the correct answer: " + correctAnswer);
            quizManager.UpdateTotalCorrectAnswers(false);
        }

        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Length)
        {
            currentQuestionIndex = questions.Length;
            quizManager.CompleteQuiz();
        }

        DisplayQuestion();
        Debug.Log("F is pressed and disabled the choicesGUI");
        guiManager.choicesGUI.SetActive(false);
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

    IEnumerator TypeDialogue(string dialogue)
    {
        guiManager.dialogueText.text = ""; // Clear the text initially

        foreach (char letter in dialogue.ToCharArray())
        {
            guiManager.dialogueText.text += letter; // Add one letter at a time
            yield return null; // Wait for a short time before adding the next letter
        }
    }

    // New coroutine for updating text with typing effect
    IEnumerator UpdateTextWithTypingEffect(string newText, System.Action onTypingComplete = null)
    {
        isTyping = true; // Set the flag to indicate that the coroutine is running
        guiManager.dialogueText.text = ""; // Clear the text initially

        foreach (char letter in newText.ToCharArray())
        {
            guiManager.dialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.01f); // Wait for a short time before adding the next letter
        }

        isTyping = false; // Reset the flag after finishing the coroutine

        // Invoke the callback function when typing is complete
        if (onTypingComplete != null)
        {
            onTypingComplete.Invoke();
        }
    }

    void UpdateDialogueText(string newText, System.Action onTypingComplete = null)
    {
        // Check if the typing coroutine is already running
        if (!isTyping)
        {
            StartCoroutine(UpdateTextWithTypingEffect(newText, onTypingComplete));
        }
        else
        {
            // If typing is in progress, disable choices GUI
            guiManager.choicesGUI.SetActive(false);
        }
    }

    void SetNPCDialogue(string npcName)
    {
        guiManager.interactGUI.SetActive(false);

        if (isQuizComplete == false)
        {
            guiManager.npcNameText.text = npcName;
            guiManager.interactText.text = npcName;

            string dialogue = "";

            if (npcName == "Mr. Dela Cruz")
            {
                canTakeQuestion = true;
                dialogue = "I am Mr. Dela Cruz and I have a quiz for you.";
            }
            else if (npcName == "Mr. Bautista")
            {
                canTakeQuestion = true;
                dialogue = "I am Mr. Bautista and I have a quiz for you.";
            }
            else if (npcName == "Ms. Gomez")
            {
                canTakeQuestion = true;
                dialogue = "I am Ms. Gomez and I have a quiz for you.";
            }
            else if (npcName == "Ms. Fernandez")
            {
                if (QuizManager.answeredQuizCount == 3)
                {
                    dialogue = "I am Ms. Fernandez and I have a quiz for you.";
                    canTakeQuestion = true;
                }
                else
                {
                    canTakeQuestion = false;
                    dialogue = "You can't take this exam yet.";
                }
            }

            guiManager.dialogueGUI.SetActive(true);

            // Check if typing coroutine is already running
            if (!isTyping)
            {
                StartCoroutine(UpdateTextWithTypingEffect(dialogue, () =>
                {
                    if (hasAcceptedQuiz)
                    {
                        isQuizActive = true;
                        DisplayQuestion();
                    }
                    else
                    {
                        StartCoroutine(ProcessPlayerInput());
                    }
                }));
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
                string declineText = "You declined the quiz.";
                UpdateDialogueText(declineText);
                yield break;
            }
        }
    }

    void HandleQuizAcceptance()
    {
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
            // Use the new coroutine to update the text with typing effect
            UpdateDialogueText(questions[currentQuestionIndex], () =>
            {
                // Update choices text when typing is complete
                SetChoicesText();
                // Show choices GUI when typing is complete
                guiManager.choicesGUI.SetActive(true);
            });

            guiManager.dialogueGUI.SetActive(true);
        }
        else
        {
            if (currentQuestionIndex == 3)
            {
                QuizManager.answeredQuizCount++;
                // Debug.Log("count ng anwswerCount" + QuizManager.answeredQuizCount);
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
    IEnumerator DeactivateQuizCoroutine()
    {
        isDeactivationCoroutineRunning = true;

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Deactivate the tutorial GameObject
        // professorScript.SetActive(false);
        collect.SetActive(true);

        isDeactivationCoroutineRunning = false;
    }
    void UpdateCorrectAnswersText()
    {
        interactingNPC = false;
        string finalScoreText = "You scored " + correctAnswersCount.ToString() + " out of " + questions.Length.ToString() + ".";
        UpdateDialogueText(finalScoreText, () =>
        {
            // Additional logic if needed after displaying the final score
            // guiManager.dialogueGUI.SetActive(false);
            quizDone = true;
            // collector.SetActive(true);
        });
    }
}