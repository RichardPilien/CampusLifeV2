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
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
        }

        public void Play()
        {
            PauseMenuCanvas.SetActive(false);
            PlayerCamera.SetActive(true);
            Time.timeScale = 1f;
            Paused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
