using System.Collections;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    public Transform player; // The Current location of the player
    public GameObject[] spawnObjects; // Astroids and Opponet Ships to be spawned
    public Transform spawnArea; 
    public float sInterval; // The amount of time in seconds to wait to spawn more opponent in 

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
            yield return new WaitForSeconds(sInterval); 
            int numberOfObjects = Random.Range(1, Mathf.FloorToInt(Time.timeSinceLevelLoad / 60f) + 2); 
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

        // Assign the player transform to the spawned opponent's OpMove script
        OpMove opMove = spawnedObject.GetComponent<OpMove>();
        if (opMove != null)
        {
            opMove.player = player;
        }
    }
}
