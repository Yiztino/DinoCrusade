using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSkins : MonoBehaviour
{

    public GameObject weapon; // Referencia al objeto del arma
    public Material[] skins; // Lista de las diferentes skins disponibles

    private int currentSkinIndex = 0;
    private Renderer weaponRenderer;

    void Start()
    {
        weaponRenderer = weapon.GetComponent<Renderer>();
        // Establecer la skin inicial
        SetWeaponSkin(currentSkinIndex);
    }

    // Método para cambiar la skin del arma
    public void SetWeaponSkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            weaponRenderer.material = skins[skinIndex];
            currentSkinIndex = skinIndex;
        }
    }

    // Método que se llama cuando se hace clic en el botón
    public void OnSkinChangeButtonClick()
    {
        // Cambiar a la siguiente skin en la lista
        currentSkinIndex = (currentSkinIndex + 1) % skins.Length;
        SetWeaponSkin(currentSkinIndex);
    }
}
