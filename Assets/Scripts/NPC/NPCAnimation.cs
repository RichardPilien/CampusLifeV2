using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class NPCAnimation : MonoBehaviour
    {
        private Animator animator;
        private NPCControllerMatthew npcControllerMatthew;
        private bool hasStartedWalking = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            npcControllerMatthew = GameObject.FindGameObjectWithTag("NPCController").GetComponent<NPCControllerMatthew>();

            // Trigger the idle animation when the game starts
            StartIdleAnimation();
        }

        void Update()
        {
            // Start walking animation if NPCController is enabled
            if (npcControllerMatthew.walking && !hasStartedWalking)
            {
                StartWalkingAnimation();
                Debug.Log("Walking");
                hasStartedWalking = true;
            }

            if (npcControllerMatthew.reachEndWaypoint == true)
            {
                Debug.Log("NPCAanimation " + npcControllerMatthew.reachEndWaypoint);
                StopWalkingAnimation();
                StartIdleAnimation();
            }

        }

        void StartIdleAnimation()
        {
            // Set the "IsIdle" parameter to true to start the idle animation
            animator.SetBool("IsIdle", true);
        }

        void StartWalkingAnimation()
        {
            // Set the "IsWalking" parameter to true to start the walking animation
            animator.SetBool("IsWalking", true);
        }

        void StopWalkingAnimation()
        {
            // Set the "IsWalking" parameter to false to stop the walking animation
            animator.SetBool("IsWalking", false);
        }
    }
}
