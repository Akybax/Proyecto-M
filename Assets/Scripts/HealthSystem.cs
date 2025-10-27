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
    // Si el jugador está haciendo parry, ignora el golpe
    PlayerCombat combat = GetComponent<PlayerCombat>();
    if (combat != null && combat.IsParrying())
    {
        Debug.Log(gameObject.name + " bloqueó el golpe!");
        return;
    }

    currentHealth -= amount;
    Debug.Log(gameObject.name + " recibió daño. Vida restante: " + currentHealth);

    if (currentHealth <= 0)
    {
        Die();
    }
}

    void Die()
    {
        Debug.Log(gameObject.name + " ha sido noqueado!");
        gameObject.SetActive(false); // desactiva al jugador
        FindObjectOfType<GameManager>()?.PlayerDefeated(gameObject);
    }
    
}
