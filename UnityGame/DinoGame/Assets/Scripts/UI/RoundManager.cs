using UnityEngine.SceneManagement;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public Canvas betweenRounds;
    public Canvas shopCanvas;

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        betweenRounds.gameObject.SetActive(true);
        print("Pausa");
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        betweenRounds.gameObject.SetActive(false);
        isGamePaused = false;
        print("Despausa xd");
    }

    public void ShowShop()
    {
        shopCanvas.gameObject.SetActive(true);
        betweenRounds.gameObject.SetActive(false);
    }

    public void HideShop()
    {
        shopCanvas.gameObject.SetActive(false);
        ResumeGame();
    }

    public void MainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
