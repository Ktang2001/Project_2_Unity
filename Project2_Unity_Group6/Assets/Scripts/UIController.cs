using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button QuitButton;
    public Toggle noiseToggle;  
    public string gameSceneName = "GameScene";  // Changeable Starter scene incase in the future we want to create more levels

    void Start()
    {
        // Assigning button click listeners
        startButton.onClick.AddListener(StartGame);// Start button 
        QuitButton.onClick.AddListener(QuitGame); // Quit button 
        noiseToggle.onValueChanged.AddListener(ToggleNoise); // Togle for volume contorl

        // Initialize toggle state
        noiseToggle.isOn = AudioListener.volume > 0;
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game 
    }
    void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("StarterScene");
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScreen"); // Loads the menu screen (Not used did not have time to implment in game ui)
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
