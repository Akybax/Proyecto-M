using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Controles (escribe las teclas)")]
    public string attackKeyName = "b";       // Player1: b  | Player2: keypad1
    public string parryKeyName = "n";        // Player1: n  | Player2: keypad2
    public Animator animator;
    public WeaponCollider weapon;            // referencia al palo

    bool isParrying = false;
    KeyCode attackKey;
    KeyCode parryKey;

    void Start()
    {
        // Convertimos el texto escrito a KeyCode real
        attackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), attackKeyName, true);
        parryKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), parryKeyName, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }

        if (Input.GetKeyDown(parryKey))
        {
            StartParry();
        }

        if (Input.GetKeyUp(parryKey))
        {
            EndParry();
        }
    }

    void Attack()
    {
        if (animator != null)
            animator.SetTrigger("Attack");

        weapon.EnableDamage();
        Invoke(nameof(StopAttack), 0.3f); // duración del golpe
    }

    void StopAttack()
    {
        weapon.DisableDamage();
    }

    void StartParry()
    {
        isParrying = true;
        if (animator != null)
            animator.SetTrigger("Parry");
        Debug.Log($"{gameObject.name} está haciendo Parry!");
    }

    void EndParry()
    {
        isParrying = false;
        Debug.Log($"{gameObject.name} terminó el Parry.");
    }

    public bool IsParrying()
    {
        return isParrying;
    }
}
