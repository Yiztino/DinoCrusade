using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySavedSkin : MonoBehaviour
{
    public SkinSelector skinSelector; // Referencia al SkinSelector

    void Start()
    {
        // Asegúrate de que la referencia al SkinSelector esté configurada correctamente
        if (skinSelector != null)
        {
            skinSelector.ApplySavedSkin();
        }
        else
        {
            Debug.LogWarning("SkinSelector reference is missing!");
        }
    }
}
