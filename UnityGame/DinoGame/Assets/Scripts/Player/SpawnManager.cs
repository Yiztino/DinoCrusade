using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    //public float spawnInterval;
    public bool spawnOnStart = true;
    public int round = 1;

    private DinosaurCounter dinosaurCounter;

    private RoundManager roundManager;

    public Canvas betweenRounds;
    public Canvas shopCanvas;

    void Start()
    {
        dinosaurCounter = FindObjectOfType<DinosaurCounter>();
        roundManager = FindAnyObjectByType<RoundManager>();

        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObjectWithInterval());
    }


    IEnumerator SpawnObjectWithInterval()
    {
        while (true)
        {
            if (dinosaurCounter != null && dinosaurCounter.AreAllObjectsDestroyed())
            {
                int objectsToSpawnThisRound = round * 5;

                for (int i = 0; i < objectsToSpawnThisRound; i++)
                {
                    int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
                    GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

                    int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                    Transform spawnPoint = spawnPoints[randomSpawnIndex];

                    Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
                }

                round++;
                if (roundManager != null)
                {
                    roundManager.PauseGame();
                }
            }

            yield return null;
        }
    }

}