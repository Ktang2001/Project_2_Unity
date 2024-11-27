using System.Collections;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    public Transform player;
    public GameObject[] spawnObjects; // A list of game objects that can be spawned.
    public Transform spawnArea;  // The gameobject representing the bondaries of where things can spawn
    public float sInterval; // # of seconds between spawn periods

    void Start()
    {
        StartCoroutine(SpawnObjectsOverTime());
    }

    void Update()
    {
        transform.position = player.position;
    }

    IEnumerator SpawnObjectsOverTime()
    {
        while (true) // Creates an infinite loop
        {
            yield return new WaitForSeconds(sInterval);  // Waits a amount of time set by the sInterval before begining another spawning round.
            int numberOfObjects = Random.Range(1, Mathf.FloorToInt(Time.timeSinceLevelLoad / 60f) + 2);  // scales the amount of objects spawned based on the amount of time that has pased to scale the difficualty over time.
            for (int i = 0; i < numberOfObjects; i++)
            {
                SpawnObject();  // This method shown below will spawn a oppoennt object and set its target to be the player
            }
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            // Selects a random x,y, and Z axis to whith in the game obejects range to ensure it is somewhat close to the player
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2), // 
            Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        GameObject objectToSpawn = spawnObjects[Random.Range(0, spawnObjects.Length)];
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Assign the player transform to the spawned opponent's OpMove script
        OpMove opMove = spawnedObject.GetComponent<OpMove>();
        if (opMove != null)
        {
            opMove.player = player;
        }
       
    }
}
