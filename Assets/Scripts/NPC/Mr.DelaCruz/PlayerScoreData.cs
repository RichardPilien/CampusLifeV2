using System;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    [System.Serializable]
    public class Reward
    {
        public string rewardName;
        public int rewardPoints;
    }

    [System.Serializable]
    public class PlayerScoreData : MonoBehaviour
    {
        public int currentGrade;
        public Reward[] rewards;

        // Example method to add score
        public void AddGrade(int grades)
        {
            currentGrade += grades;
            Debug.Log("Score added: " + grades + ", Total Score: " + currentGrade);
        }

        // Example method to give a reward
        public void GiveReward(string rewardName)
        {
            Reward reward = Array.Find(rewards, r => r.rewardName == rewardName);

            if (reward != null)
            {
                currentGrade += reward.rewardPoints;
                Debug.Log("Reward given: " + rewardName + ", Points added: " + reward.rewardPoints + ", Total Score: " + currentGrade);
            }
            else
            {
                Debug.LogWarning("Reward not found: " + rewardName);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Add any additional initialization logic here
        }

        // Update is called once per frame
        void Update()
        {
            // Add any update logic here
        }
    }
}
