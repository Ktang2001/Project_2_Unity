using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button QuitButton;
    public Toggle noiseToggle;  
    public string gameSceneName = "GameScene";  

    void Start()
    {
        // Assigning button click listeners
        startButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(QuitGame);
        noiseToggle.onValueChanged.AddListener(ToggleNoise);

        // Initialize toggle state
        noiseToggle.isOn = AudioListener.volume > 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("StarterScene");
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    void ToggleNoise(bool isOn)
    {
        
        if (isOn)
        {
            // Enable noise
            AudioListener.volume = 1.0f;
        }
        else
        {
            // Disable noise
            AudioListener.volume = 0.0f;
        }
    }
}
