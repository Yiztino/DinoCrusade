using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;

    private PlayerController playerController;
    private GunSystem gunSystem;

    private void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            gunSystem = player.GetComponentInChildren<GunSystem>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseGame();
            print("jaja");
        }
    }

    public void Cancel()
    {
        ResumeGame();
    }

    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        canvas.SetActive(true);

        if (gunSystem != null)
        {
            gunSystem.enabled = false;
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        canvas.SetActive(false);

        if (gunSystem != null)
        {
            gunSystem.enabled = true;
        }

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        Time.timeScale = 1;
    }

    public void Mapa1(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();

    }

    public void Mapa2(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();

    }

    public void Mapa3(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();

    }

    public void Mapa4(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ResumeGame();

    }
}
