using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public GameObject weapon; // Referencia al objeto del arma
    public Material[] grassSkins; // Materiales para las skins de hierba
    public Material[] fireSkins; // Materiales para las skins de fuego
    public Material[] waterSkins; // Materiales para las skins de agua

    private Renderer weaponRenderer;

    void Start()
    {
        weaponRenderer = weapon.GetComponent<Renderer>();
    }

    // Método para cambiar la skin del arma a la de hierba
    public void SetGrassSkin(int skinIndex)
    {
        SetWeaponSkin(grassSkins, skinIndex);
    }

    // Método para cambiar la skin del arma a la de fuego
    public void SetFireSkin(int skinIndex)
    {
        SetWeaponSkin(fireSkins, skinIndex);
    }

    // Método para cambiar la skin del arma a la de agua
    public void SetWaterSkin(int skinIndex)
    {
        SetWeaponSkin(waterSkins, skinIndex);
    }

    // Método para cambiar la skin del arma
    private void SetWeaponSkin(Material[] skins, int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            weaponRenderer.material = skins[skinIndex];
        }
    }
}
