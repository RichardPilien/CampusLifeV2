using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Basic3rdPersonMovementAndCamera
{
    [Serializable]
    public class PlayerIdleData
    {
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}
