using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class FootStepScript : MonoBehaviour
    {
        public GameObject footstep;
        private AudioSource audioSource; // Assuming you are using AudioSource for footsteps sound

        // Set the default pitch value
        private float defaultPitch = 1.0f;
        private float shiftPitch = 1.2f;
        private float ctrlPitch = 0.5f;

        private bool ctrlPressed = false; // Track if Ctrl key is pressed
        private bool shiftPressed = false; // Track if Shift key is pressed
        private float shiftPressTime = 0.0f;
        private float shiftThreshold = 2.0f; // Time in seconds to maintain Shift pitch

        private bool spacePressed = false; // Track if Space key is pressed
        private float spaceCooldown = 0.8f; // Cooldown duration for space key

        // Start is called before the first frame update
        void Start()
        {
            footstep.SetActive(false);
            audioSource = footstep.GetComponent<AudioSource>(); // Assuming AudioSource is attached to the footstep GameObject
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
            {
                footsteps();
            }
            else
            {
                StopFootsteps();
            }

            // Check for space key press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DisableAudioForDuration());
            }
        }

        IEnumerator DisableAudioForDuration()
        {
            if (!spacePressed)
            {
                spacePressed = true;
                audioSource.enabled = false;
                yield return new WaitForSeconds(spaceCooldown);
                audioSource.enabled = true;
                spacePressed = false;
            }
        }

        void footsteps()
        {
            footstep.SetActive(true);

            // Check if the shift key is pressed
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                shiftPressed = true;
                shiftPressTime = Time.time;
                audioSource.pitch = shiftPitch;
            }

            // Check if the ctrl key is pressed
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                ctrlPressed = !ctrlPressed; // Toggle the state of ctrlPressed
                if (ctrlPressed)
                {
                    audioSource.pitch = ctrlPitch;
                }
                else
                {
                    // If Shift is not pressed or released before the threshold, reset pitch to default
                    if (!shiftPressed || Time.time - shiftPressTime < shiftThreshold)
                    {
                        audioSource.pitch = defaultPitch;
                    }
                }
            }
        }

        void StopFootsteps()
        {
            footstep.SetActive(false);
            // Reset the pitch value when footsteps stop
            audioSource.pitch = defaultPitch;
            ctrlPressed = false; // Reset the Ctrl toggle state
            shiftPressed = false; // Reset the Shift toggle state
        }
    }
}
