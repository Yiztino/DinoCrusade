using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int totalScore;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        DinoAI.OnDinoKilled += AddScore;
    }

    private void OnDisable()
    {
        DinoAI.OnDinoKilled -= AddScore;
    }

    public void AddScore(int points)
    {
        totalScore += points;
        Debug.Log("Score: " + totalScore);
        UpdateScoreText(); 
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
    }
}
