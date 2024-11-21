using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void OpenOptions()
    {
        // Implement options menu functionality here
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
