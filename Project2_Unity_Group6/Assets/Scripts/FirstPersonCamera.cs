using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offSet = new Vector3(0, 0, 1); // This Vairable represents the amount of distance in the x,y,and z axis we want the object to follow the object from
                                                     // If you make it public you can adjust the postion from the unity editor


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

        // This will allows the camera to follow the vehicle
        transform.position = player.transform.position + offSet;
            // transfom.postion for the object refrences as well as the player it is ment to follow just represents the postion of the object refrenced 
            // The offSet is a Vector we have defined above to represent the distance the object needs to follow the player from
            // So in effect we are setting the position of object based on the postion of the target game object + the offset vector distances. 
            // The only main diffrence between this and the Follow Player script is that it based on a global positon instead of local based on player location 
        
        transform.rotation = player.transform.rotation;
            // This allows the camera to always face the same direction as the Character
            // One thing of note is that unlike the third person camera it does not need to look at the location it just needs to have the same orientation

    }
}
