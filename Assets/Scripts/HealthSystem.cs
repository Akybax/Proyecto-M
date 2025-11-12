using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        PlayerCombat combat = GetComponent<PlayerCombat>();
        if (combat != null && combat.IsParrying())
        {
            Debug.Log(gameObject.name + " bloqueó el golpe!");
           
             return;
        }

        currentHealth -= amount;

        SFXManager.Instance.PlayDaño();
        
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha sido noqueado!");

        // Primero notifica al GameManager
        FindObjectOfType<GameManager>()?.PlayerDefeated(gameObject);

        // Luego desactiva al jugador
        gameObject.SetActive(false);
    }
}
