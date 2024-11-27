using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 0.0f;
    public float pitchSpeed = 50.0f;
    public float yawSpeed = 50.0f;
    public float rollSpeed = 50.0f;
    public float minSpeed = 0.0f;
    public float maxSpeed = 50.0f;
    public float acceleration = 2.0f;
    private float verticalInput;
    private float horizontalInput;
    private float rollInput;
    private float speedInput;
    public Camera camera1; // Third person camera
    public Camera camera2; // First person camera
    private bool isCamera1Active = true;
    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDistance = 100f;
    public LayerMask hitLayers; // Hit able layers we just set this to opponent 
    public GameObject impactEffectPrefab;
    public float laserDamage = 10f;
    private GameObject currentLaser;
    public Health playerHealth;

    void Start()
    {
        speed = 0.0f;

        if (playerHealth == null)
        {
            playerHealth = GetComponent<Health>(); // sets player health to be what the health script component is set to 
        }

        // Lets the game start in third person control 
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }

    void Update()
    {
        // Switch between cameras when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCamera1Active = !isCamera1Active; // switches the view to the the other camera based on if camera 1 is not active it makes it active otherwise it deactivtes its

            // based on which one is active the code deactivates the toher 
            camera1.gameObject.SetActive(isCamera1Active);
            camera2.gameObject.SetActive(!isCamera1Active);
        }

        // Player Inputs
        verticalInput = Input.GetAxis("Vertical"); // Pitch Controls  w, and s
        horizontalInput = Input.GetAxis("Horizontal"); // Yaw Controls a, and d
        rollInput = Input.GetAxis("Roll"); // Roll Controls q, and e
        speedInput = Input.GetAxis("Throttle"); // Changes speed of the player based on the  up and down arrows

        // Adjust speed based on input and limited based on min and max speed values for the ship.
        if (speedInput != 0.0f)
        {
            speed += speedInput * acceleration;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }

        if (speed != 0.0f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // Rotate the spaceships pitch, Yaw, and roll 
        transform.Rotate(Vector3.right, pitchSpeed * verticalInput * Time.deltaTime); 
        transform.Rotate(Vector3.up, yawSpeed * horizontalInput * Time.deltaTime);    
        transform.Rotate(Vector3.forward, rollSpeed * rollInput * Time.deltaTime);    

        // Laser controls
        if (Input.GetMouseButtonDown(0))
        {
            StartLaser();
        }
        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopLaser();
        }
    }

    void StartLaser()
    {
        currentLaser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }


    // So the player can fire a continous beam this updated fuction allows the game to calcuale the new location of the fireing point and target location.
    void UpdateLaser()
    {
        if (currentLaser != null)
        {
            Camera currentCamera = isCamera1Active ? camera1 : camera2;
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, laserDistance, hitLayers))
            {
                // Hit logic if it hits an opponent or astroind.
                DrawLaser(hit.point); // This reather then being about where the mouse is is based on where the laser impacted the opposition allowing for the laser to apper to stop at the oppent rather then traveling through
                CreateImpactEffect(hit.point); // Impact particle affect
                Health opponentHealth = hit.collider.GetComponent<Health>(); // Call the Health componet of the opponent 
                opponentHealth.TakeDamage(laserDamage); // Calls specifically the take dame from the health script  of the opponent
            }
            else
            {
                // Allows the laser to still be visiable even if it is a miss be forgoing the hit logic 
                Vector3 targetPosition = ray.origin + ray.direction * laserDistance;
                DrawLaser(targetPosition); // This draw laser draws the laser based on mouse postion allone
            }
        }
    }

    void StopLaser()
    {
        // Stops laser visial when not engaged
        if (currentLaser != null)
        {
            Destroy(currentLaser);
        }
    }

    void DrawLaser(Vector3 targetPosition)
    {
        // Makes the laser visible on screen to the player
        LineRenderer lr = currentLaser.GetComponent<LineRenderer>();
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, targetPosition);
    }

    void CreateImpactEffect(Vector3 impactPoint)
    {
        Instantiate(impactEffectPrefab, impactPoint, Quaternion.identity);
    }
}
