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
            //Die();
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


    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}

