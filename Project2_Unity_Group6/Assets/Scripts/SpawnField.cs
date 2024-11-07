using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    public Transform player; // The Current location of the player
    public GameObject[] spawnObjects; // List of possible Gameobects to be spawned into the game
    public Transform spawnArea;// The area covered by an added Gameobject to this field that will allow anything to spawn inside it.
    public float sInterval; // The amount of time in seconds 
    
   
    
    void Start()
    {
        StartCoroutine(SpawnObjectsOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;// Has the spawnfield fallow the player whereever he goes.
    }
    IEnumerator SpawnObjectsOverTime() {
        while (true) // Creates an infinate loop 
        {
            yield return new WaitForSeconds(sInterval); // Pauses the effect based on the amont of time added as the interval.
            int numberOfObjects = Random.Range(1, Mathf.FloorToInt(Time.timeSinceLevelLoad / 60f) + 2); // Scales the amount of objects added based on the amount of time that has Passed
            for (int i = 0; i < numberOfObjects; i++)
            {
                SpawnObject(); // Spawns the objects 
            }
        }
    }
    void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
            Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        GameObject objectToSpawn = spawnObjects[Random.Range(0, spawnObjects.Length)];
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        
        OpMove opponentMove = spawnedObject.GetComponent<OpMove>(); 
        if (opponentMove != null) { opponentMove.player = player; }
    }

}
