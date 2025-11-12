using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Configuración de daño")]
    public int damage = 1;
    public string enemyTag = "Player";

    private bool canDealDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;
        if (!other.CompareTag(enemyTag)) return;

        // Obtenemos los componentes del objetivo
        PlayerCombat enemyCombat = other.GetComponent<PlayerCombat>();
        HealthSystem health = other.GetComponent<HealthSystem>();

        // Si el enemigo hace parry, no recibe daño ni chichón
        if (enemyCombat != null && enemyCombat.IsParrying())
        {
            Debug.Log($"{other.name} bloqueó el golpe con parry!");
            SFXManager.Instance.PlayParry();
            return;
        }

        // ⚡ Primero mostramos el efecto visual del golpe (chichón)
        if (enemyCombat != null && other.gameObject.activeInHierarchy)
        {
            enemyCombat.RecibirGolpe();
        }

        // ❤️ Luego aplicamos el daño (podría desactivar el objeto)
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log($"{gameObject.name} golpeó a {other.name}");
        }
    }

    // Activar o desactivar la capacidad de causar daño
    public void EnableDamage() => canDealDamage = true;
    public void DisableDamage() => canDealDamage = false;
}
