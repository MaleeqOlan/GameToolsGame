using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCollectable : MonoBehaviour
{
    // OnTriggerEnter is called when the Collider other enters the trigger. Checks if the player has collided with the speed boost collectable
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("SpeedBoostCollectable: Player collided with speed boost collectable");
        }
    }
}
