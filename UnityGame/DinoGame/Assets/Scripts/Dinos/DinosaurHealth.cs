using TMPro;
using UnityEngine;

public class DinosaurHealth : MonoBehaviour
{
    public int vidaMaxima;
    private int vidaActual;

    private Animator myAnim;
    private MeshCollider body;

    private FollowPlayer followPlayer;

    public int Points;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI pointsTextRound;
    public TextMeshProUGUI killsText;

    private bool isDead = false;
    private int kills;


    private void Start()
    {
        vidaActual = vidaMaxima;
        myAnim = GetComponent<Animator>();
        body = GetComponent<MeshCollider>();
        followPlayer = GetComponent<FollowPlayer>();
        pointsText = GameObject.Find("FPS_UI/Points").GetComponent<TextMeshProUGUI>();
        pointsTextRound = GameObject.Find("BetweenRounds/BG/Points").GetComponent<TextMeshProUGUI>();
        killsText = GameObject.Find("BetweenRounds/BG/Kills").GetComponent<TextMeshProUGUI>();
        if (pointsText == null)
        {
            Debug.LogError("No se encontró el TextMeshProUGUI para los puntos.");
        }
        if (pointsTextRound == null)
        {
            Debug.LogError("No se encontró el TextMeshProUGUI para el resumen de puntos.");
        }
    }

    public void TakeDamage(int dmgAmout)
    {
        vidaActual -= dmgAmout;

        if (vidaActual <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (isDead) return;

        isDead = true;
        followPlayer.StopMoving();
        body.enabled = false;
        myAnim.SetBool("isDead", true);

        Transform mouthTransform = transform.Find("DinosaurMouth");
        if (mouthTransform != null)
        {
            GameObject mouthObject = mouthTransform.gameObject;
            BoxCollider mouthCollider = mouthObject.GetComponent<BoxCollider>();
            if (mouthCollider != null)
            {
                mouthCollider.enabled = false;
            }
        }

        AddKill();
        AddPoints(Points);
        Destroy(gameObject, 5);
    }

    public void AddPoints(int pointsToAdd)
    {
        Points = PlayerPrefs.GetInt("PlayerPoints", 0);
        Points += pointsToAdd;
        pointsText.text = "Points: " + Points.ToString();
        pointsTextRound.text = "Points: " + Points.ToString();
        PlayerPrefs.SetInt("PlayerPoints", Points);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ResetPoints();
            ResetKills();
        }
    }

    public void AddKill()
    {
        kills = PlayerPrefs.GetInt("Kills", 0);
        kills ++;
        killsText.text = "Kills: " + kills.ToString();
        PlayerPrefs.SetInt("Kills", kills) ;
        PlayerPrefs.Save();
    }

    public void ResetPoints()
    {
        PlayerPrefs.DeleteKey("PlayerPoints");
        Points = 0;
        pointsText.text = "Points: " + Points.ToString();
    }

    public void ResetKills()
    {
        PlayerPrefs.DeleteKey("Kills");
        kills = 0;
        killsText.text = "Kills: " + Points.ToString();
    }

}