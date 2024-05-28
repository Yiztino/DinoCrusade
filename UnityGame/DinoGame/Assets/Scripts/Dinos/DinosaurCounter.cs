using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinosaurCounter : MonoBehaviour
{
    public TextMeshProUGUI countText, killsText; // Nueva variable para el contador de kills

    public int objectCount, killsCount; // Nueva variable para llevar el conteo de kills

    void Start()
    {
        UpdateObjectCount();
        UpdateKillsText(); // Inicializamos el texto de kills
    }

    void Update()
    {
        if (ObjectCountChanged())
        {
            UpdateObjectCount();
        }
    }

    public void UpdateObjectCount()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DinosaurBody");
        objectCount = objects.Length;
        UpdateCountText();
    }

    void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "Dinosaurs remaining:  " + objectCount.ToString();
        }
    }

    bool ObjectCountChanged()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DinosaurBody");
        return objects.Length != objectCount;
    }

    public bool AreAllObjectsDestroyed()
    {
        return objectCount == 0;
    }

    // Método para incrementar el contador de kills y actualizar el texto
    public void IncrementKillsCount()
    {
        killsCount++;
        UpdateKillsText();
    }

    // Método para actualizar el texto del contador de kills
    void UpdateKillsText()
    {
        if (killsText != null)
        {
            killsText.text = "Kills: " + killsCount.ToString();
        }
    }
}
