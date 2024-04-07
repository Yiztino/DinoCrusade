using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array de objetos que quieres instanciar
    public Transform[] spawnPoints; // Lugares donde quieres que aparezcan los objetos
    public float spawnInterval = 2f; // Intervalo de tiempo entre instancias
    public bool spawnOnStart = true; // ¿Quieres que comience a spawnear objetos al iniciar?

    void Start()
    {
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    // Método para iniciar el proceso de spawnear objetos
    public void StartSpawning()
    {
        StartCoroutine(SpawnObjectWithInterval());
    }

    // Método para detener el proceso de spawnear objetos
    public void StopSpawning()
    {
        StopCoroutine(SpawnObjectWithInterval());
    }

    // Corrutina para spawnear objetos con intervalo de tiempo
    IEnumerator SpawnObjectWithInterval()
    {
        while (true)
        {
            // Espera el intervalo de tiempo antes de instanciar el siguiente objeto
            yield return new WaitForSeconds(spawnInterval);

            // Obtiene un índice aleatorio para elegir un objeto aleatorio del array
            int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

            // Obtiene un índice aleatorio para elegir un punto de spawn aleatorio
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            // Instancia el objeto en el punto de spawn aleatorio
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
