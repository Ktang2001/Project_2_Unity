using UnityEngine;

public class OpMove : MonoBehaviour
{
    public Transform player;  
    public float speed = 5f;  
    public float rotationSpeed = 5f; 
    public float pitchSpeed = 5f;  
    public float stopDistance = 2f;  
    public float laserRange = 10f; 
    public LineRenderer laserLine;  
    public Transform laserStartPoint; 
    private Vector3 sphereCenter;  

    void Start()
    {
        sphereCenter = transform.position;  
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
            CheckAndShootLaser();
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

    void CheckAndShootLaser()
    {
        if (Vector3.Distance(sphereCenter, player.position) <= laserRange)
        {
            ShootLaser();
        }
        else
        {
            laserLine.enabled = false; 
        }
    }

    void ShootLaser()
    {
        laserLine.enabled = true;  
        laserLine.SetPosition(0, laserStartPoint.position);  
        laserLine.SetPosition(1, player.position);  
    }
}
