using UnityEngine;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    [Header("Prefab del sombrero que se agregará al jugador")]
    public GameObject sombreroPrefab;

    // Diccionario que guarda las recompensas por nombre de jugador
    private Dictionary<string, int> recompensasJugadores = new Dictionary<string, int>();

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

    // 🔹 Llamado cuando un jugador gana una ronda
    public void DarRecompensa(GameObject jugadorGanador)
    {
        if (jugadorGanador == null)
        {
            Debug.LogWarning("RewardManager: jugadorGanador es NULL");
            return;
        }

        string nombre = jugadorGanador.name;

        if (!recompensasJugadores.ContainsKey(nombre))
            recompensasJugadores[nombre] = 0;

        recompensasJugadores[nombre]++;
        Debug.Log($"🎉 {nombre} ganó un sombrero. Total acumulado: {recompensasJugadores[nombre]}");
    }

    // 🔹 Llamado al iniciar una nueva ronda para aplicar todas las recompensas
    public void AplicarRecompensas(GameObject jugador)
    {
        if (jugador == null)
        {
            Debug.LogWarning("RewardManager: jugador es NULL, no se puede aplicar recompensa.");
            return;
        }

        if (sombreroPrefab == null)
        {
            Debug.LogError("🚫 No hay prefab de sombrero asignado en el RewardManager.");
            return;
        }

        string nombre = jugador.name;

        if (!recompensasJugadores.ContainsKey(nombre))
        {
            Debug.Log($"⚠️ {nombre} no tiene recompensas registradas.");
            return;
        }

        int cantidad = recompensasJugadores[nombre];
        if (cantidad <= 0)
        {
            Debug.Log($"ℹ️ {nombre} no tiene sombreros aún.");
            return;
        }

        // 🔹 Buscar el punto de anclaje "Cabeza"
        Transform puntoCabeza = jugador.GetComponentInChildren<Transform>(true);
        puntoCabeza = FindChildRecursive(jugador.transform, "Cabeza");

        if (puntoCabeza == null)
        {
            Debug.LogWarning($"⚠️ {nombre} no tiene objeto 'Cabeza'. Se usará el transform del jugador.");
            puntoCabeza = jugador.transform;
        }

        // 🧹 Eliminar sombreros anteriores
        foreach (Transform hijo in puntoCabeza)
        {
            if (hijo.name.StartsWith("Sombrero"))
                Destroy(hijo.gameObject);
        }

        // 🧢 Instanciar tantos sombreros como victorias tenga el jugador
        for (int i = 0; i < cantidad; i++)
        {
            GameObject sombrero = Instantiate(sombreroPrefab, puntoCabeza);
            sombrero.name = "Sombrero_" + (i + 1);

            // 📍 Posicionar cada sombrero un poco más alto que el anterior
            sombrero.transform.localPosition = Vector3.up * (0.18f * i);
            sombrero.transform.localRotation = Quaternion.identity;
            sombrero.transform.localScale = Vector3.one * 1.0f;
        }

        Debug.Log($"🧢 {nombre} tiene ahora {cantidad} sombrero(s) apilados correctamente sobre 'Cabeza'");
    }

    // 🔹 Reiniciar todas las recompensas (opcional)
    public void ReiniciarRecompensas()
    {
        recompensasJugadores.Clear();
        Debug.Log("🎯 Todas las recompensas han sido reiniciadas.");
    }
    // 🔍 Búsqueda recursiva para encontrar un hijo por nombre en toda la jerarquía
private Transform FindChildRecursive(Transform parent, string childName)
{
    foreach (Transform child in parent)
    {
        if (child.name == childName)
            return child;

        Transform result = FindChildRecursive(child, childName);
        if (result != null)
            return result;
    }
    return null;
}

}
