using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic3rdPersonMovementAndCamera
{
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player entered the trigger zone of this item
                // You can implement logic to handle the collection here
                // For example, you can destroy the item or update a collection counter
                // You can also notify the CollectTaskGiver script about the collection.
                Debug.Log("Player is near!");
            }
        }
    }
}
