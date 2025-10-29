using UnityEngine;
using System.Collections; // necesario para usar corutinas

public class PlayerCombat : MonoBehaviour
{
    [Header("Controles (escribe las teclas)")]
    public string attackKeyName = "b";       // Player1: b  | Player2: keypad1
    public string parryKeyName = "n";        // Player1: n  | Player2: keypad2
    public Animator animator;
    public WeaponCollider weapon;            // referencia al palo

    [Header("Sistema de Chichones")]
    public GameObject[] chichones;           // arrastra los 3 chichones aquí
    private int golpes = 0;

    bool isParrying = false;
    KeyCode attackKey;
    KeyCode parryKey;

    void Start()
    {
        // Convertimos el texto escrito a KeyCode real
        attackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), attackKeyName, true);
        parryKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), parryKeyName, true);

        // Asegurarnos de que los chichones estén ocultos al inicio
        foreach (GameObject c in chichones)
            if (c != null) c.SetActive(false);
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
        Invoke(nameof(StopAttack), 1f); // duración del golpe
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

    // ============================================================
    // 🧠 SISTEMA DE CHICHONES
    // ============================================================

    public void RecibirGolpe()
    {
        if (golpes < chichones.Length)
        {
            StartCoroutine(AparecerChichon(chichones[golpes]));
            golpes++;
        }

        if (golpes >= chichones.Length)
        {
            ReiniciarRonda();
        }
    }

    IEnumerator AparecerChichon(GameObject chichon)
    {
        chichon.SetActive(true);
        chichon.transform.localScale = Vector3.zero;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 5f;
            chichon.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }
    }

    void ReiniciarRonda()
    {
        Debug.Log($"{gameObject.name} perdió la ronda!");
        golpes = 0;

        // Ocultamos todos los chichones
        foreach (GameObject c in chichones)
            if (c != null) c.SetActive(false);

        // Aquí podrías resetear posiciones, animaciones o invocar un GameManager
    }
}
