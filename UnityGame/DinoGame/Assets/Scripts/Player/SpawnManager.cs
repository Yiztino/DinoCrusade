using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2f; 
    public bool spawnOnStart = true; 

    void Start()
    {
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObjectWithInterval());
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnObjectWithInterval());
    }

    IEnumerator SpawnObjectWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
