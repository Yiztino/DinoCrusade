using UnityEngine;
using System.Collections;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    public bool spawnOnStart = true;
    public int round = 1;
    public int pointsPerRound = 10;  // Puntos añadidos por cada ronda completada
    public TextMeshProUGUI roundPointsText;  // Referencia al TextMeshProUGUI para mostrar los puntos

    private DinosaurCounter dinosaurCounter;
    private RoundManager roundManager;

    public Canvas betweenRounds;
    public Canvas shopCanvas;

    void Start()
    {
        dinosaurCounter = FindObjectOfType<DinosaurCounter>();
        roundManager = FindAnyObjectByType<RoundManager>();

        // Inicializar el texto de puntos al inicio
        UpdatePointsText();

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

                // Incrementar la ronda y pausar el juego
                round++;
                if (roundManager != null)
                {
                    roundManager.PauseGame();
                }

                // Agregar puntos por la ronda completada y guardarlos en PlayerPrefs
                int roundPoints = PlayerPrefs.GetInt("Round Points", 0);
                roundPoints += pointsPerRound;
                PlayerPrefs.SetInt("Round Points", roundPoints);
                PlayerPrefs.Save();

                // Actualizar el texto de puntos
                UpdatePointsText();
            }

            yield return null;
        }
    }

    // Método para actualizar el texto de puntos
    void UpdatePointsText()
    {
        if (roundPointsText != null)
        {
            int roundPoints = PlayerPrefs.GetInt("Round Points", 0);
            roundPointsText.text = "Points: " + roundPoints.ToString();
        }
    }
}
