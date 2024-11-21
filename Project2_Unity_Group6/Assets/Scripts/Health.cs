using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
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
        if (gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
        else if (gameObject.CompareTag("Opponent"))
        {
            GameManager.Instance.IncreaseScore(10); 
        }

        Destroy(gameObject);
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}

