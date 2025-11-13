using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    private bool roundRestarting = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 0f; // Empieza en pausa (si quieres menú inicial)
        Debug.Log("⏸ Juego iniciado en pausa (Awake)");
    }

    void Start()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        asyncLoad.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        Time.timeScale = 0f;
        Debug.Log("⏸ Escena cargada y juego en pausa");

        BuscarJugadores();
    }

    public void PlayerDefeated(GameObject defeatedPlayer)
    {
        if (roundRestarting) return;
        roundRestarting = true;

        GameObject ganador = (defeatedPlayer == player1) ? player2 : player1;
        Debug.Log("Jugador derrotado: " + defeatedPlayer.name);

        if (ganador != null && RewardManager.Instance != null)
            RewardManager.Instance.DarRecompensa(ganador);

        // 🔊 comenta si no usas SFX
        // SFXManager.Instance.PlayFinRonda();

        Invoke(nameof(RestartRound), 2f);
    }

    private void RestartRound()
    {
        SceneManager.UnloadSceneAsync("SampleScene").completed += (op) =>
        {
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive).completed += (op2) =>
            {
                roundRestarting = false;

                // ✅ Reasignar jugadores recién cargados
                BuscarJugadores();

                // ✅ Aplicar recompensas
                if (RewardManager.Instance != null)
                {
                    if (player1 != null)
                        RewardManager.Instance.AplicarRecompensas(player1);

                    if (player2 != null)
                        RewardManager.Instance.AplicarRecompensas(player2);
                }
            };
        };
    }

    // ---------------------------------------------------------------------
    // 🔹 Método que busca y reasigna los jugadores automáticamente
    private void BuscarJugadores()
    {
        // Busca por nombre exacto (asegúrate de que se llamen igual en la jerarquía)
        player1 = GameObject.Find("player1");
        player2 = GameObject.Find("player2");

        if (player1 == null || player2 == null)
        {
            Debug.LogWarning("⚠️ No se pudieron encontrar uno o ambos jugadores en la nueva escena.");
        }
        else
        {
            Debug.Log("✅ Jugadores reasignados correctamente tras recargar escena.");
        }
    }
}
