using UnityEngine;


// This code is ment to create fireflicker effect when a laser hits an opponet or astroid
public class FireLightFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1.0f;
    public float maxIntensity = 3.0f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        fireLight = GetComponent<Light>(); // Gets the light componet of the effect to mess with its intensity
    }

    void Update()
    {
        if (fireLight != null)
        {
            float flicker = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
            fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, flicker);
        }
    }
}
