using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public int damage = 1;
    public string enemyTag = "Player";
    private bool canDealDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;
        if (!other.CompareTag(enemyTag)) return;

        HealthSystem health = other.GetComponent<HealthSystem>();
        PlayerCombat enemyCombat = other.GetComponent<PlayerCombat>();

        // Si el enemigo está haciendo parry, no recibe daño
        if (enemyCombat != null && enemyCombat.IsParrying())
        {
            Debug.Log($"{other.name} bloqueó el golpe con parry!");
            return;
        }

        // Si tiene sistema de vida, aplica daño
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log($"{gameObject.name} golpeó a {other.name}");
        }

        // 👇 Aquí añadimos el chichón visual
        if (enemyCombat != null)
        {
            enemyCombat.RecibirGolpe();
        }
    }

    public void EnableDamage() => canDealDamage = true;
    public void DisableDamage() => canDealDamage = false;
}
