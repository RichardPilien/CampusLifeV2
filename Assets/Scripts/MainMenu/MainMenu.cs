using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basic3rdPersonMovementAndCamera
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Player Has Quit The Game");
        }
    }
}
