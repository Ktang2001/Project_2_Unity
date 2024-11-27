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
    public float laserDamage = 10f;
    public float damageInterval = 1f; 
    public float laserCooldown = 5f;  

    private GameObject currentLaser;
    private float nextDamageTime = 0f;
    private float nextFireTime = 0f;

    void Start()
    {
        sphereCenter = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            // The AI for this focususes on the player so it only updates all of this if a player is set by the spawnfield script.
            MoveTowardsPlayer();
            RotateTowardsPlayer();
            CheckPlayerInSphereAndShoot();
        }
    }

    void MoveTowardsPlayer()
    {
        // This navigates the oppoent the player 
        Vector3 direction = (player.position - transform.position).normalized; // gives a rough distance between the player and opponent and based on the values provided for each of the three axises it creates a direction for the oppoennt to head in.

        if (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime; // Makes sure the oppoenet has to travel the distance rather then just appearing next to the payer 
        }
    }

    void RotateTowardsPlayer()
    {
        // This code ensures the opponent is allways orientated towards the player
        Vector3 direction = player.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Pitch adjustment
            Vector3 targetEulerAngles = targetRotation.eulerAngles;
            targetEulerAngles.x = Mathf.LerpAngle(transform.eulerAngles.x, targetEulerAngles.x, pitchSpeed * Time.deltaTime); // sets the x angle, however, ensure is has to rotate at a certian speed so it does not orientated imedately
            targetEulerAngles.y = Mathf.LerpAngle(transform.eulerAngles.y, targetEulerAngles.y, rotationSpeed * Time.deltaTime);// sets the y angle, however, ensure is has to rotate at a certian speed so it does not orientated imedately
            targetEulerAngles.z = Mathf.LerpAngle(transform.eulerAngles.z, targetEulerAngles.z, rotationSpeed * Time.deltaTime);// sets the z angle, however, ensure is has to rotate at a certian speed so it does not orientated imedately
            transform.eulerAngles = targetEulerAngles;
        }
    }

    void CheckPlayerInSphereAndShoot() 
    {
        if (Vector3.Distance(transform.position, player.position) <= laserDistance && Time.time > nextFireTime)
        {
            // Checks to see if the player is in range if so it calls the shoot at player method
            ShootLaserAtPlayer();
            nextFireTime = Time.time + laserCooldown; // This applies a cool down to give the player an advantage
        }
    }

    void ShootLaserAtPlayer()
    {
        if (currentLaser != null) // gets rid of previous lasers shot at player if they still exist
        {
            Destroy(currentLaser);
        }
        currentLaser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation); // Creates a new laser instance

        Vector3 targetPosition = player.position;
        DrawLaser(targetPosition); // Draws laser based on the target (Player ) postion
        CreateImpactEffect(targetPosition); // craetes impact effect based on player position.

        if (Time.time >= nextDamageTime) // This is where we deal damage to the player we were originally going to do a damage per second system but ran out of time still used the method to deal normal one hit on instance of 10 damage
        {
            DealContinuousDamage(); 
            nextDamageTime = Time.time + damageInterval; // WIP
        }
    }

    void DealContinuousDamage()
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(laserDamage);
        }
    }

    void DrawLaser(Vector3 targetPosition) // This method draws the laser to make visable for the player
    {
        LineRenderer lr = currentLaser.GetComponent<LineRenderer>();
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, targetPosition);
    }

    void CreateImpactEffect(Vector3 impactPoint)
    {
        GameObject impactEffect = Instantiate(impactEffectPrefab, impactPoint, Quaternion.identity);
        Destroy(impactEffect, 2f);  // Adjust the duration as needed
    }
}
