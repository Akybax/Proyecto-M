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
        DontDestroyOnLoad(gameObject); // <-- Opcional: mantiene el GameManager entre escenas
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
    }
}
    void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); // <-- Juego es la 0
    }

    public void PlayerDefeated(GameObject defeatedPlayer)
    {
        if (roundRestarting) return; // Evita que se llame dos veces

        roundRestarting = true;
        Debug.Log("Jugador derrotado: " + defeatedPlayer.name);

        // Esperar 2 segundos antes de reiniciar
        Invoke(nameof(RestartRound), 2f);
    }

    private void RestartRound()
    {     
            SceneManager.UnloadSceneAsync(0);
            SceneManager.UnloadSceneAsync(1);       
            SceneManager.LoadSceneAsync(0); 
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); // <-- Juego es la 0
    }
}
