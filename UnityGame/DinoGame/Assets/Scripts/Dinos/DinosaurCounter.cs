using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinosaurCounter : MonoBehaviour
{
    public string objectTag; 
    public TextMeshProUGUI countText; 

    private int objectCount; 
    void Start()
    {
        UpdateObjectCount();
    }

    void Update()
    {
        if (ObjectCountChanged())
        {
            UpdateObjectCount();
        }
    }

    void UpdateObjectCount()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(objectTag);
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
        GameObject[] objects = GameObject.FindGameObjectsWithTag(objectTag);
        return objects.Length != objectCount;
    }
}
