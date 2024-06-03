using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public GameObject weapon; // Referencia al objeto del arma
    public Material[] grassSkins; // Materiales para las skins de hierba
    public Material[] fireSkins; // Materiales para las skins de fuego
    public Material[] waterSkins; // Materiales para las skins de agua

    private Renderer weaponRenderer;

    private const string SkinTypeKey = "SkinType";
    private const string SkinIndexKey = "SkinIndex";

    void Start()
    {
        weaponRenderer = weapon.GetComponent<Renderer>();
        LoadWeaponSkin();
    }

    // Método para cambiar la skin del arma a la de hierba
    public void SetGrassSkin(int skinIndex)
    {
        SetWeaponSkin(grassSkins, skinIndex);
        SaveWeaponSkin("Grass", skinIndex);
    }

    // Método para cambiar la skin del arma a la de fuego
    public void SetFireSkin(int skinIndex)
    {
        SetWeaponSkin(fireSkins, skinIndex);
        SaveWeaponSkin("Fire", skinIndex);
    }

    // Método para cambiar la skin del arma a la de agua
    public void SetWaterSkin(int skinIndex)
    {
        SetWeaponSkin(waterSkins, skinIndex);
        SaveWeaponSkin("Water", skinIndex);
    }

    // Método para cambiar la skin del arma
    private void SetWeaponSkin(Material[] skins, int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            Material selectedSkin = skins[skinIndex];
            Material[] weaponMaterials = weaponRenderer.materials;

            for (int i = 0; i < weaponMaterials.Length; i++)
            {
                weaponMaterials[i] = selectedSkin;
            }

            weaponRenderer.materials = weaponMaterials;
        }
    }

    // Método para guardar la skin seleccionada en PlayerPrefs
    private void SaveWeaponSkin(string skinType, int skinIndex)
    {
        PlayerPrefs.SetString(SkinTypeKey, skinType);
        PlayerPrefs.SetInt(SkinIndexKey, skinIndex);
        PlayerPrefs.Save();
    }

    // Método para cargar la skin seleccionada desde PlayerPrefs
    private void LoadWeaponSkin()
    {
        if (PlayerPrefs.HasKey(SkinTypeKey) && PlayerPrefs.HasKey(SkinIndexKey))
        {
            string skinType = PlayerPrefs.GetString(SkinTypeKey);
            int skinIndex = PlayerPrefs.GetInt(SkinIndexKey);

            switch (skinType)
            {
                case "Grass":
                    SetWeaponSkin(grassSkins, skinIndex);
                    break;
                case "Fire":
                    SetWeaponSkin(fireSkins, skinIndex);
                    break;
                case "Water":
                    SetWeaponSkin(waterSkins, skinIndex);
                    break;
            }
        }
    }

    // Método para aplicar la skin cargada (puede ser llamado manualmente después de cambiar de escena)
    public void ApplySavedSkin()
    {
        LoadWeaponSkin();
    }
}
