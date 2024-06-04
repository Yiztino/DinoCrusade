using UnityEngine;
using System.Collections;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    public bool spawnOnStart = true;
    public int round;
    public int pointsPerRound = 10;  
    public TextMeshProUGUI roundPointsText;
    public int maxRounds;

    private DinosaurCounter dinosaurCounter;
    private RoundManager roundManager;
    private bool gameEnded = false;


    void Start()
    {
        dinosaurCounter = FindObjectOfType<DinosaurCounter>();
        roundManager = FindAnyObjectByType<RoundManager>();

        UpdatePointsText();

        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObjectWithInterval());
    }

    IEnumerator SpawnObjectWithInterval()
    {
        int uwu = 1;

        while (round <= maxRounds && !gameEnded)
        {
            if (dinosaurCounter != null && (dinosaurCounter.AreAllObjectsDestroyed()))
            {

                if (dinosaurCounter.objectCount == 0 && uwu !=1)
                {
                    roundManager.PauseGame();
                }

                int objectsToSpawnThisRound = round * 3;

                for (int i = 0; i < objectsToSpawnThisRound; i++)
                {
                    int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
                    GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

                    int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                    Transform spawnPoint = spawnPoints[randomSpawnIndex];

                    Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
                }

                round++;

                int roundPoints = PlayerPrefs.GetInt("Round Points", 0);
                roundPoints += pointsPerRound;
                PlayerPrefs.SetInt("Round Points", roundPoints);
                PlayerPrefs.Save();

                UpdatePointsText();
            }

            uwu = 2;
            StopCoroutine("SpawnObjectWithInterval");
            yield return null;
        }

        TheEnd();
    }

    void TheEnd()
    {
        gameEnded = true;

        print("el fiiiiin");

        roundManager.GoodEnding();
    }

    void UpdatePointsText()
    {
        if (roundPointsText != null)
        {
            int roundPoints = PlayerPrefs.GetInt("Round Points", 0);
            roundPointsText.text = "Points: " + roundPoints.ToString();
        }
    }
}
