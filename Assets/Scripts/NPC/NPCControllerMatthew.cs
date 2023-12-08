using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class NPCControllerMatthew : MonoBehaviour
    {
        public Transform[] waypoints;
        public float speed;
        public float rotationSpeed; // Added rotation speed

        private int currentWaypoint = 0;
        private float delayTimer = 0f;
        public float delayTime; // Adjust this value to set the delay time
        public bool walking;
        public bool reachEndWaypoint;
        // private DialogueManager dialogueManager;

        void Update()
        {
            if (currentWaypoint < waypoints.Length)
            {
                if (delayTimer <= 0f)
                {
                    walking = true;
                    MoveToWaypoint();
                }
                else
                {
                    // Rotate even during the waiting period
                    RotateTowards(waypoints[currentWaypoint].position);
                    delayTimer -= Time.deltaTime;
                }
            }
            else
            {
                // Print a debug log when the NPC reaches the last waypoint
                reachEndWaypoint = true;
                Debug.Log("Reached end? = " + reachEndWaypoint);
            }
        }

        void MoveToWaypoint()
        {
            // Get the current waypoint position
            Vector3 targetPosition = waypoints[currentWaypoint].position;

            // Calculate the direction and move towards the waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Rotate to face the waypoint
            RotateTowards(targetPosition);

            // Check if the NPC has reached the waypoint
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Print a debug log when the NPC reaches a waypoint
                // Debug.Log($"Reached waypoint {currentWaypoint}");

                // Reset the delay timer
                delayTimer = delayTime;

                // Move to the next waypoint
                currentWaypoint++;

                // Print a debug log when the NPC starts moving to the next waypoint
                // Debug.Log($"Moving to waypoint {currentWaypoint}");
            }
            else
            {
                // Print a debug log indicating that the NPC is walking
                walking = true;
            }
        }

        void RotateTowards(Vector3 targetPosition)
        {
            // Calculate the direction to the target
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Create a rotation to face the target
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Apply the rotation to the NPC with rotation speed
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
