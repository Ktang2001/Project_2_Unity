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
        verticalInput = Input.GetAxis("Vertical"); // Pitch Controls  w,and s
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
    }
}
