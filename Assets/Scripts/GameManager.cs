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
    }

    void Start()
    {
        // Asegúrate de que las escenas estén en Build Settings (índice 0 = base, 1 = juego)
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void PlayerDefeated(GameObject defeatedPlayer)
    {
        if (roundRestarting) return;

        roundRestarting = true;
        Debug.Log("Jugador derrotado: " + defeatedPlayer.name);

        // Esperar 2 segundos antes de reiniciar
        Invoke(nameof(RestartRound), 2f);
    }

private void RestartRound()
{
    Debug.Log("Reiniciando SampleScene...");

    // 🔹 Recarga solo la escena de juego
    SceneManager.UnloadSceneAsync("SampleScene");
    SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);

    roundRestarting = false;
}

}
