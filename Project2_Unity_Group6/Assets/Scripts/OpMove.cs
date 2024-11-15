using UnityEngine;

public class OpMove : MonoBehaviour
{
    public Transform player;  // Assign the player's transform in the Inspector
    public float speed = 5f;  // Speed of the opponent
    public float rotationSpeed = 5f;  // Speed of the rotation
    public float pitchSpeed = 5f;  // Speed of pitch adjustment
    public float stopDistance = 2f;  // Distance to stop from the player

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Pitch adjustment
            Vector3 targetEulerAngles = targetRotation.eulerAngles;
            targetEulerAngles.x = Mathf.LerpAngle(transform.eulerAngles.x, targetEulerAngles.x, pitchSpeed * Time.deltaTime);
            targetEulerAngles.y = Mathf.LerpAngle(transform.eulerAngles.y, targetEulerAngles.y, rotationSpeed * Time.deltaTime);
            targetEulerAngles.z = Mathf.LerpAngle(transform.eulerAngles.z, targetEulerAngles.z, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = targetEulerAngles;
        }
    }
}
