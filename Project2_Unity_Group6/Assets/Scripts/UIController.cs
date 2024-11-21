using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Toggle noiseToggle;
    public string optionsSceneName = "OptionsScene";  
    public string gameSceneName = "GameScene";  

    void Start()
    {
        // Assigning button click listeners
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(LoadOptionsScene);
        noiseToggle.onValueChanged.AddListener(ToggleNoise);

        // Initialize toggle state
        noiseToggle.isOn = AudioListener.volume > 0;
    }

    void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("StarterScene");
    }

    void LoadOptionsScene()
    {
        SceneManager.LoadScene("OptionsScreen");
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
