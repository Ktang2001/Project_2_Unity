using UnityEngine;

public class OpMove : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float speed = 5f; // Speed of the opponent ship
    public float rotationSpeed = 5f; // Speed of the rotation
    public float stopDistance = 2f; // Distance to stop from the player

    void Update()
    {
        if (player != null)
        {
            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the opponent ship towards the player if it is not too close
            if (Vector3.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position += direction * speed * Time.deltaTime;
            }

            // Rotate the opponent ship to face the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
