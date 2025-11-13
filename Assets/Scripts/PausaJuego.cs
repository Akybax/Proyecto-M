using UnityEngine;

public class PausaJuego : MonoBehaviour
{
    private bool juegoPausado;

    void Start()
    {
        // 🔹 Detecta si el juego ya está pausado por el GameManager
        juegoPausado = (Time.timeScale == 0f);
    }

    public void PausarReanudar()
    {
        // Actualiza el estado real antes de cambiarlo
        juegoPausado = (Time.timeScale == 0f);

        if (juegoPausado)
        {
            // Estaba pausado → reanudar
            Time.timeScale = 1f;
            juegoPausado = false;
            Debug.Log("▶ Reanudar juego");
        }
        else
        {
            // Estaba corriendo → pausar
            Time.timeScale = 0f;
            juegoPausado = true;
            Debug.Log("⏸ Pausar juego");
        }
    }
}
