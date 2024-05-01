using UnityEngine;

[CreateAssetMenu(fileName = "New Dino Data", menuName = "Dino Data")]
public class DinoData : ScriptableObject
{
    [SerializeField] private string species;
    [SerializeField] private int detectionZone;
    [SerializeField] private int stamina;
    [SerializeField] private int velocity;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private GameObject[] hostileTowards;
    [SerializeField] private GameObject[] nonHostileTowards;

    public string SpeciesName { get { return species; } }

    public int DetectionZone { get { return detectionZone; } }

    public int Stamina { get { return stamina; } }

    public int Velocity { get { return velocity; } }

    public int Damage { get { return damage; } }

    public int Health { get { return health; } }

    public GameObject[] HostileTowards { get { return hostileTowards; } }

    public GameObject[] NonHostileTowards { get { return nonHostileTowards; } }
}
