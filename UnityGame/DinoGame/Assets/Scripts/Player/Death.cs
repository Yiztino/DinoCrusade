using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(sceneName);
    }
}
