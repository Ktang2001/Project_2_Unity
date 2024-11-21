using UnityEngine;

public class Op_wep_sphere : MonoBehaviour
{
    public Transform opponent;  

    void Update()
    {
        if (opponent != null)
        {
            Follow();
        }
    }

    void Follow()
    {
        // Keep the sphere centered on the opponent
        transform.position = opponent.position;
    }
}
