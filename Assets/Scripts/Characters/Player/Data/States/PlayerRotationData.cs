using System;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    [Serializable]
    public class PlayerRotationData
    {  
        [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
    }
}
