using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25.00f;
    public float turnRate = 5f;
    public float rotateRate = 5f;
    private float vertInput;
    private float horzInput;
    private float wheelInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertInput = Input.GetAxis("Vertical");
        horzInput = Input.GetAxis("Horizontal");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");

        transform.Rotate(Vector3.up * Time.deltaTime * rotateRate * vertInput);
        transform.Rotate(Vector3.right * Time.deltaTime * turnRate * horzInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * wheelInput);




    }
}
