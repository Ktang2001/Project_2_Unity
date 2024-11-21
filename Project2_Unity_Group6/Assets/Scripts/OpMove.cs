using UnityEngine;

public class OpMove : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float pitchSpeed = 5f;
    public float stopDistance = 2f;
    private Vector3 sphereCenter;

    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDistance = 100f;
    public LayerMask hitLayers;
    public GameObject impactEffectPrefab;  

    private GameObject currentLaser;

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
            CheckPlayerInSphereAndShoot();
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

    void CheckPlayerInSphereAndShoot()
    {
        if (Vector3.Distance(transform.position, player.position) <= laserDistance)
        {
            ShootLaserAtPlayer();
        }
    }

    void ShootLaserAtPlayer()
    {
        if (currentLaser == null)
        {
            currentLaser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        }

        Vector3 targetPosition = player.position;
        DrawLaser(targetPosition);
        CreateImpactEffect(targetPosition);
    }

    void DrawLaser(Vector3 targetPosition)
    {
        LineRenderer lr = currentLaser.GetComponent<LineRenderer>();
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, targetPosition);
    }

    void CreateImpactEffect(Vector3 impactPoint)
    {
        Instantiate(impactEffectPrefab, impactPoint, Quaternion.identity);
    }
}
