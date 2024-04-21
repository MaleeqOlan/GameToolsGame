using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public Transform player; // Reference to the player's transform

    private bool playerDetected = false;

    void Update()
    {
        // Check if the player is within the detection range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        // If the player is detected, move towards the player's position
        if (playerDetected)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
