using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offSet = new Vector3(0, 5, -15); // This Vairable represents the amount of distance in the x,y,and z axis we want the object to follow the object from
                                                    // If you make it public you can adjust the postion from the unity editor


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

        // This will allows the camera to follow the vehicle based on a local corrdinate with the origin set the center of the player
        transform.position = player.transform.TransformPoint(offSet);
            // transfom.postion for the object refrences as well as the player it is ment to follow just represents the postion of the object refrenced 
            // The offSet is a Vector we have defined above to represent the distance the object needs to follow the player from
            // So in effect we are setting the position of object based on the postion of the target game object + the offset vector distances. 
        
        transform.LookAt(player.transform.position); // This line allows the camera to continue to look at the player. Even as the player changes their orientaion

    }
}
