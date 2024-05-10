using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinosaurCounter : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public int objectCount;

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
}
