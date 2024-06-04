using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public GameObject weapon; // Referencia al objeto del arma
    public Material baseSkin; // Material base para la skin del arma
    public Material[] goldSkins; // Materiales para las skins de hierba
    public Material[] fireSkins; // Materiales para las skins de fuego
    public Material[] waterSkins; // Materiales para las skins de agua

    private Renderer weaponRenderer;

    private const string SkinTypeKey = "SkinType";
    private const string SkinIndexKey = "SkinIndex";

    private Dictionary<string, Material[]> skins;

    void Start()
    {
        weaponRenderer = weapon.GetComponent<Renderer>();

        // Inicializar el diccionario de skins
        skins = new Dictionary<string, Material[]>
        {
            { "Gold", goldSkins },
            { "Fire", fireSkins },
            { "Water", waterSkins }
        };

        LoadWeaponSkin();
    }

    public void SetGoldSkin(int skinIndex)
    {
        SetWeaponSkin("Gold", skinIndex);
    }

    public void SetFireSkin(int skinIndex)
    {
        SetWeaponSkin("Fire", skinIndex);
    }

    public void SetWaterSkin(int skinIndex)
    {
        SetWeaponSkin("Water", skinIndex);
    }

    private void SetWeaponSkin(string skinType, int skinIndex)
    {
        if (skins.ContainsKey(skinType) && skinIndex >= 0 && skinIndex < skins[skinType].Length)
        {
            Material selectedSkin = skins[skinType][skinIndex];
            Material[] weaponMaterials = weaponRenderer.materials;

            for (int i = 0; i < weaponMaterials.Length; i++)
            {
                weaponMaterials[i] = selectedSkin;
            }

            weaponRenderer.materials = weaponMaterials;
            SaveWeaponSkin(skinType, skinIndex);
            Debug.Log($"Skin cambiada a: {skinType} con índice: {skinIndex}");
        }
        else
        {
            Debug.LogWarning("Índice de skin fuera de rango o tipo de skin desconocido");
        }
    }

    private void SaveWeaponSkin(string skinType, int skinIndex)
    {
        PlayerPrefs.SetString(SkinTypeKey, skinType);
        PlayerPrefs.SetInt(SkinIndexKey, skinIndex);
        PlayerPrefs.Save();
        Debug.Log($"Skin guardada: {skinType} con índice: {skinIndex}");
    }

    private void LoadWeaponSkin()
    {
        if (PlayerPrefs.HasKey(SkinTypeKey) && PlayerPrefs.HasKey(SkinIndexKey))
        {
            string skinType = PlayerPrefs.GetString(SkinTypeKey);
            int skinIndex = PlayerPrefs.GetInt(SkinIndexKey);

            SetWeaponSkin(skinType, skinIndex);
        }
        else
        {
            SetBaseSkin();
            Debug.Log("No se encontró skin guardada, aplicando skin base");
        }
    }

    private void SetBaseSkin()
    {
        Material[] weaponMaterials = weaponRenderer.materials;

        for (int i = 0; i < weaponMaterials.Length; i++)
        {
            weaponMaterials[i] = baseSkin;
        }

        weaponRenderer.materials = weaponMaterials;
        Debug.Log("Skin base aplicada");
    }

    public void ApplySavedSkin()
    {
        LoadWeaponSkin();
    }
}
