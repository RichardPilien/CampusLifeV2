using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI totalCorrectAnswersText; // UI text element to display the total number of correct answers from all NPCs
    public TextMeshProUGUI totalQuestionsText; // UI text element to display the total number of questions from all NPCs
    public TextMeshProUGUI percentageText; // UI text element to display the percentage of correct answers

    public int totalQuestions = 0; // Variable to store the total number of questions
    private int totalCorrectAnswers = 0; // Variable to store the total count of correct answers
    public bool quizCompleted = false; // Flag to track if the quiz is completed

    void Start()
    {
        totalCorrectAnswersText.text = "Total Correct Answers: " + totalCorrectAnswers.ToString();
        totalQuestionsText.text = "Total Questions: " + totalQuestions.ToString();
        percentageText.text = "Percentage: 0%";
    }

    public void UpdateTotalCorrectAnswers(bool isCorrect)
    {
        if (isCorrect)
        {
            totalCorrectAnswers++;
        }

        totalCorrectAnswersText.text = "Total Correct Answers: " + totalCorrectAnswers.ToString();

        if (quizCompleted)
        {
            // Calculate the percentage
            float percentage = (float)totalCorrectAnswers / totalQuestions * 100;
            percentageText.text = "Percentage: " + percentage.ToString("F1") + "%";
        }
    }

    public void UpdateTotalQuestions(int count)
    {
        totalQuestions += count;
        totalQuestionsText.text = "Total Questions: " + totalQuestions.ToString();
    }

    public void CompleteQuiz()
    {
        quizCompleted = true;

        // Calculate the final percentage
        float percentage = (float)totalCorrectAnswers / totalQuestions * 100;
        percentageText.text = "Percentage: " + percentage.ToString("F1") + "%";
    }
}
