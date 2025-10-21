using UnityEngine;

public class PAtk : MonoBehaviour
{
    public GameObject opponent; // Referencia al oponente
    public float attackForce = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Botón de ataque
        {
            Attack();
        }
    }

    void Attack()
    {
        Vector3 direction = (opponent.transform.position - transform.position).normalized;
        Rigidbody opponentRb = opponent.GetComponent<Rigidbody>();
        opponentRb.AddForce(direction * attackForce, ForceMode.Impulse);
    }
}
