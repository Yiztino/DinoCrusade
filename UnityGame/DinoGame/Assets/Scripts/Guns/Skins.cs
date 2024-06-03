using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NuevaSkin", menuName ="Personaje")]
public class Skins : ScriptableObject
{
    public GameObject personajeJugable;

    public Sprite imagen;

    public string nombre;
}
