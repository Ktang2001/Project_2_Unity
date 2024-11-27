using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerScore = 0;
    public string gameOverSceneName; 
    public Text scoreText;

    void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroyes any other instance of game manager (There should never be another one but its just and addtional check)
        }
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Player Score: " + playerScore);
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene(gameOverSceneName); 
    }
}
