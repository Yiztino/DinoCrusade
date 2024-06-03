using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinosaurCounter : MonoBehaviour
{
    public TextMeshProUGUI countText, killsText; 

    public int objectCount, killsCount; 

    void Start()
    {
        UpdateObjectCount();
        UpdateKillsText(); 
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

    public void IncrementKillsCount()
    {
        killsCount++;
        UpdateKillsText();
    }

    void UpdateKillsText()
    {
        if (killsText != null)
        {
            killsText.text = "Kills: " + killsCount.ToString();
        }
    }
}
