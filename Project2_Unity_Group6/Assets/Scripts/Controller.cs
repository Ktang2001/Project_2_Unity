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

    public Camera camera1;
    public Camera camera2;
    private bool isCamera1Active = true;

    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDistance = 100f;
    public LayerMask hitLayers;
    public GameObject impactEffectPrefab;  // Reference to the impact effect prefab

    private GameObject currentLaser;
    private GameObject hoveredOpponent;

    void Start()
    {
        speed = 0.0f;

        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }

    void Update()
    {
        // Switch between cameras when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCamera1Active = !isCamera1Active;

            camera1.gameObject.SetActive(isCamera1Active);
            camera2.gameObject.SetActive(!isCamera1Active);
        }

        // Player Inputs
        verticalInput = Input.GetAxis("Vertical"); // Pitch Controls  w, and s
        horizontalInput = Input.GetAxis("Horizontal"); // Yaw Controls a, and d
        rollInput = Input.GetAxis("Roll"); // Roll Controls q, and e
        speedInput = Input.GetAxis("Mouse ScrollWheel"); // Scrolling the mouse wheel can speed it up 

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

        // Rotate the spaceship
        transform.Rotate(Vector3.right, pitchSpeed * verticalInput * Time.deltaTime);  // Pitch
        transform.Rotate(Vector3.up, yawSpeed * horizontalInput * Time.deltaTime);    // Yaw
        transform.Rotate(Vector3.forward, rollSpeed * rollInput * Time.deltaTime);    // Roll

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

        // Highlight opponent on hover
        HighlightOpponentOnHover();

        // Select opponent on click
        if (Input.GetMouseButtonDown(1)) // Right mouse button to select
        {
            SelectOpponent();
        }
    }

    void StartLaser()
    {
        currentLaser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }

    void UpdateLaser()
    {
        if (currentLaser != null)
        {
            Camera currentCamera = isCamera1Active ? camera1 : camera2;
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, laserDistance, hitLayers))
            {
                DrawLaser(hit.point);
                CreateImpactEffect(hit.point);
                MakeImpactGlow(hit);
                if (hit.collider.CompareTag("Opponent"))
                {
                    // Additional logic for hitting an opponent can be added here
                }
            }
            else
            {
                Vector3 targetPosition = ray.origin + ray.direction * laserDistance;
                DrawLaser(targetPosition);
            }
        }
    }

    void StopLaser()
    {
        if (currentLaser != null)
        {
            Destroy(currentLaser);
        }
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
        Destroy(impactEffectPrefab, 5f);
    }

    void MakeImpactGlow(RaycastHit hit)
    {
        Renderer renderer = hit.collider.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            if (material.HasProperty("_EmissionColor"))
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", Color.red * 2.0f); // Adjust the color and intensity as needed
            }
        }
    }

    void HighlightOpponentOnHover()
    {
        Camera currentCamera = isCamera1Active ? camera1 : camera2;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Opponent"))
            {
                // Highlight the opponent
                if (hoveredOpponent != null && hoveredOpponent != hit.collider.gameObject)
                {
                    ResetHighlight(hoveredOpponent);
                }
                hoveredOpponent = hit.collider.gameObject;
                Renderer renderer = hoveredOpponent.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow; // Change to yellow to indicate hover
                }
            }
            else
            {
                if (hoveredOpponent != null)
                {
                    ResetHighlight(hoveredOpponent);
                    hoveredOpponent = null;
                }
            }
        }
        else
        {
            if (hoveredOpponent != null)
            {
                ResetHighlight(hoveredOpponent);
                hoveredOpponent = null;
            }
        }
    }

    void ResetHighlight(GameObject opponent)
    {
        Renderer renderer = opponent.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white; // Reset to original color
        }
    }

    void SelectOpponent()
    {
        Camera currentCamera = isCamera1Active ? camera1 : camera2;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Opponent"))
            {
                Debug.Log("Opponent selected: " + hit.collider.gameObject.name);
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red; // Change to red to indicate selection
                }

                // Ensure the laser can hit the selected opponent
                laserDistance = Vector3.Distance(firePoint.position, hit.point);
            }
        }
    }
}
