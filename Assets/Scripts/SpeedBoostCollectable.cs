using System.Collections;
using UnityEngine;

public class SpeedBoostCollectable : MonoBehaviour
{
    public float boostDuration = 5f; // Duration of the speed boost in seconds
    public float boostMultiplier = 2f; // Multiplier for the speed boost

    // OnTriggerEnter is called when the Collider other enters the trigger. Checks if the player has collided with the speed boost collectable
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the InfiniteCarController component from the player
            InfiniteCarController carController = other.gameObject.GetComponent<InfiniteCarController>();
            if (carController != null)
            {
                // Apply the speed boost
                carController.StartCoroutine(ApplySpeedBoost(carController));
                // Destroy the speed boost collectable object
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ApplySpeedBoost(InfiniteCarController carController)
    {
        // Increase the move speed temporarily
        carController.moveSpeed *= boostMultiplier;

        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(boostDuration);

        // Restore the original move speed
        carController.moveSpeed /= boostMultiplier;
    }
}
