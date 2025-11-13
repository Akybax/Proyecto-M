using UnityEngine;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    // Prefab del sombrero que se agregará al jugador
    public GameObject sombreroPrefab;

    // Diccionario que guarda los jugadores y la cantidad de sombreros que tienen
    private Dictionary<GameObject, int> recompensasJugadores = new Dictionary<GameObject, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Llamar cuando alguien gane la ronda
    public void DarRecompensa(GameObject jugadorGanador)
    {
        if (!recompensasJugadores.ContainsKey(jugadorGanador))
        {
            recompensasJugadores[jugadorGanador] = 0;
        }

        recompensasJugadores[jugadorGanador]++;
        Debug.Log("🎉 " + jugadorGanador.name + " ganó un sombrero. Total: " + recompensasJugadores[jugadorGanador]);
    }

    // Llamar al iniciar la ronda para aplicar todos los sombreros acumulados
    public void AplicarRecompensas(GameObject jugador)
    {
        if (sombreroPrefab == null) return;

        if (recompensasJugadores.ContainsKey(jugador))
        {
            int cantidad = recompensasJugadores[jugador];

            for (int i = 0; i < cantidad; i++)
            {
                GameObject sombrero = Instantiate(sombreroPrefab);
                sombrero.transform.SetParent(jugador.transform);

                // Colocar los sombreros en altura, uno sobre otro
                sombrero.transform.localPosition = Vector3.up * (1.5f + i * 0.5f); 
                sombrero.transform.localRotation = Quaternion.identity;
            }
        }
    }

    // Reinicia las recompensas si quieres limpiar al final del juego
    public void ReiniciarRecompensas(GameObject jugador)
    {
        if (recompensasJugadores.ContainsKey(jugador))
        {
            recompensasJugadores[jugador] = 0;
        }
    }
}
