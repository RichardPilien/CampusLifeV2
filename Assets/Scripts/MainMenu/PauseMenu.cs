using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basic3rdPersonMovementAndCamera
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool Paused = false;
        public GameObject PauseMenuCanvas;
        public GameObject PlayerCamera;

        private bool f1IsClicked;
        private bool f2IsClicked;
        private QuizManager quizManager;
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            EnablePlayerGradeAndIDGUI();
            EnableTaskInfoGUI();
            quizManager = GameObject.FindGameObjectWithTag("QuizManager").GetComponent<QuizManager>();

            // Check if either F1 or F2 is clicked before allowing the Escape key
            if (!(f1IsClicked || f2IsClicked) && Input.GetKeyDown(KeyCode.Escape))
            {
                if (Paused)
                {
                    Play();
                }
                else
                {
                    Stop();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        void Stop()
        {
            PauseMenuCanvas.SetActive(true);
            PlayerCamera.SetActive(false);
            Time.timeScale = 0f;
            Paused = true;
            f1IsClicked = true;
            f2IsClicked = true;
        }

        public void Play()
        {
            PauseMenuCanvas.SetActive(false);
            PlayerCamera.SetActive(true);
            Time.timeScale = 1f;
            Paused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            f1IsClicked = false;
            f2IsClicked = false;
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        public void EnablePlayerGradeAndIDGUI()
        {
            if (Input.GetKeyDown(KeyCode.F2) && f1IsClicked == false)
            {
                f2IsClicked = !f2IsClicked;

                quizManager.PlayerGradeAndIDGUI.SetActive(!quizManager.PlayerGradeAndIDGUI.activeSelf);
                PlayerCamera.SetActive(!PlayerCamera.activeSelf);

                Time.timeScale = Time.timeScale == 0f ? 1f : 0f;

                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        public void EnableTaskInfoGUI()
        {
            if (Input.GetKeyDown(KeyCode.F1) && f2IsClicked == false)
            {
                f1IsClicked = !f1IsClicked;

                quizManager.TaskInfoGUI.SetActive(!quizManager.TaskInfoGUI.activeSelf);
                PlayerCamera.SetActive(!PlayerCamera.activeSelf);

                Time.timeScale = Time.timeScale == 0f ? 1f : 0f;

                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
    }
}
