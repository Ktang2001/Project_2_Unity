using UnityEngine;

public class FireLightFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1.0f;
    public float maxIntensity = 3.0f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        fireLight = GetComponent<Light>();
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
