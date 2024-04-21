using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Chase, DriveAlongside };
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player
    public float maxTurnAngle = 45f; // Maximum angle the enemy can turn in degrees
    public float bumpForce = 10f; // Force applied to the player upon collision
    public float bumpCooldown = 2f; // Cooldown period between bumps
    public float driveAlongsideProbability = 0.3f; // Probability of the AI driving alongside the player
    public Transform player; // Reference to the player's transform

    private bool playerDetected = false;
    private bool isOnCooldown = false;

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

        // If the player is detected and the AI is not on cooldown, move and turn towards the player
        if (playerDetected && !isOnCooldown)
        {
            AIState currentState = AIState.Chase;

            // Randomly determine whether to drive alongside the player
            if (Random.value < driveAlongsideProbability)
            {
                currentState = AIState.DriveAlongside;
            }

            switch (currentState)
            {
                case AIState.Chase:
                    // Calculate direction to the player
                    Vector3 chaseDirection = (player.position - transform.position).normalized;

                    // Calculate angle to rotate towards the player
                    float targetChaseAngle = Mathf.Atan2(chaseDirection.x, chaseDirection.z) * Mathf.Rad2Deg;

                    // Calculate the angle between the enemy's forward vector and the direction to the player
                    float chaseAngleDifference = Mathf.DeltaAngle(transform.eulerAngles.y, targetChaseAngle);

                    // Clamp the angle difference to limit the maximum turn angle
                    float clampedChaseAngleDifference = Mathf.Clamp(chaseAngleDifference, -maxTurnAngle, maxTurnAngle);

                    // Rotate the enemy towards the player's direction
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + clampedChaseAngleDifference, 0f);

                    // Move the enemy forward
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;

                case AIState.DriveAlongside:
                    // Calculate direction perpendicular to the player's direction
                    Vector3 playerDirection = player.position - transform.position;
                    Vector3 perpendicularDirection = new Vector3(-playerDirection.z, 0, playerDirection.x).normalized;

                    // Calculate angle to rotate to drive alongside the player
                    float targetAlongsideAngle = Mathf.Atan2(perpendicularDirection.x, perpendicularDirection.z) * Mathf.Rad2Deg;

                    // Calculate the angle between the enemy's forward vector and the direction to drive alongside the player
                    float alongsideAngleDifference = Mathf.DeltaAngle(transform.eulerAngles.y, targetAlongsideAngle);

                    // Clamp the angle difference to limit the maximum turn angle
                    float clampedAlongsideAngleDifference = Mathf.Clamp(alongsideAngleDifference, -maxTurnAngle, maxTurnAngle);

                    // Rotate the enemy towards the direction to drive alongside the player
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + clampedAlongsideAngleDifference, 0f);

                    // Move the enemy forward
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player and the AI is not on cooldown
        if (collision.gameObject.CompareTag("Player") && !isOnCooldown)
        {
            // Apply a force to the player to simulate the bump
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 bumpDirection = (collision.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(bumpDirection * bumpForce, ForceMode.Impulse);
            }

            // Start the cooldown period
            isOnCooldown = true;
            Invoke("ResetCooldown", bumpCooldown);
        }
    }

    void ResetCooldown()
    {
        // Reset the cooldown flag after the cooldown period
        isOnCooldown = false;
    }
}
