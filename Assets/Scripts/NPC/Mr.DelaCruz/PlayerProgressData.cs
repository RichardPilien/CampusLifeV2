using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class PlayerProgressData : MonoBehaviour
    {
        public int currentGrade;
        public void AddGrade(int grades)
        {
            currentGrade += grades;
            Debug.Log("Score added: " + grades + ", Total Score: " + currentGrade);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
