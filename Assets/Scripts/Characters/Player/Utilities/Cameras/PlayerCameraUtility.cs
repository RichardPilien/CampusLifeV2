using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

namespace Basic3rdPersonMovementAndCamera
{
    [Serializable]
    public class PlayerCameraUtility
    {
        [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
        [field: SerializeField] public float DefaultHorizontalWaitTime { get; private set; } = 0f;
        [field: SerializeField] public float DefaultHorizontalRecenteringTime { get; private set; } = 4f;
        private CinemachinePOV cinemachinePOV;

        public void Initialize()
        {
            cinemachinePOV = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }

        public void EnableRecentering(float WaitTime = -1f, float RecenteringTime = -1f, float baseMovementSpeed = 1f, float movementSpeed = 1f)
        {
            cinemachinePOV.m_HorizontalRecentering.m_enabled = true;

            cinemachinePOV.m_HorizontalRecentering.CancelRecentering();

            if (WaitTime == -1f)
            {
                WaitTime = DefaultHorizontalWaitTime;
            }

            if (RecenteringTime == -1f)
            {
                RecenteringTime = DefaultHorizontalRecenteringTime;
            }

            RecenteringTime = RecenteringTime * baseMovementSpeed / movementSpeed;

            cinemachinePOV.m_HorizontalRecentering.m_WaitTime = WaitTime;
            cinemachinePOV.m_HorizontalRecentering.m_RecenteringTime = RecenteringTime;

        }

        public void DisableRecentering()
        {
            cinemachinePOV.m_HorizontalRecentering.m_enabled = false;
        }
    }
}
