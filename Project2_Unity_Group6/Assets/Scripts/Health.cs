using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) // Allows damage logic to take affect 
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Player")
        {
             GameManager.Instance.GameOver(); // calls game over method
             Destroy(gameObject); // Destroyes the player gameobeject
        }
        else if (gameObject.tag == "Opponent")
        {
            GameManager.Instance.IncreaseScore(10); // crases player score by 10 
            Destroy(gameObject); // destroyes opponenet object
        }
            Die();
        }
    }

    public void Heal(float amount) // Heal in case we want to add future heal powerups to the game not used with current project
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {

        // Check if it's the player or opponent dying
        if (gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Opponent")
        {
            GameManager.Instance.IncreaseScore(10); 
            Destroy(gameObject);
        }
    }


    public float GetCurrentHealth() // To help with the creation of health bars or other health UI systems 
    {
        return currentHealth;
    }
}

