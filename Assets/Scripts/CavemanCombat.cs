using UnityEngine;

public class CavemanCombat : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 1.0f;
    private float nextAttackTime = 0f;

    void Update()
    {
        // Ataque con clic izquierdo
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }

        // Parry con clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            Parry();
        }
    }

    void Attack()
    {
        if (animator != null)
            animator.SetTrigger("Attack");
        Debug.Log("Cavernícola ataca 🪓");
    }

    void Parry()
    {
        if (animator != null)
            animator.SetTrigger("Parry");
        Debug.Log("Cavernícola hace parry 🛡️");
    }
}
