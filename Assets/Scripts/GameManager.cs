using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool roundRestarting = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // mantiene el GameManager entre escenas
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Pausar inmediatamente
        Time.timeScale = 0f;
        Debug.Log("⏸ Juego iniciado en pausa (Awake)");
    }

    void Start()
    {
        // Carga la escena y asegura que quede pausada al terminar de cargar
        var asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        asyncLoad.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        // 🔹 Pausar otra vez por si algún script la reanudó al cargar
        Time.timeScale = 0f;
        Debug.Log("⏸ Escena cargada y juego en pausa");
    }

    public void PlayerDefeated(GameObject defeatedPlayer)
    {
        if (roundRestarting) return;

        roundRestarting = true;
        Debug.Log("Jugador derrotado: " + defeatedPlayer.name);

        SFXManager.Instance.PlayFinRonda();
        Invoke(nameof(RestartRound), 2f);
    }

    private void RestartRound()
    {
        Debug.Log("Reiniciando SampleScene...");

        SceneManager.UnloadSceneAsync("SampleScene");
        var asyncLoad = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        asyncLoad.completed += (op) =>
        {
            roundRestarting = false;
          
        };
    }
}
