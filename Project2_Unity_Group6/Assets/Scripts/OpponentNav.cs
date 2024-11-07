using UnityEngine;
using UnityEngine.AI;

public class OpponentNav : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float moveSpeed = 5f; // Speed at which the spaceship moves
    public float rotationSpeed = 360f; // Maximum rotation speed in degrees per second

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.updateRotation = false; // We will handle the rotation manually
    }

    void Update()
    {
        if (player != null)
        {
            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);

            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Rotate towards the player over time based on rotation speed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Move forward
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}




