using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Cierra el juego (en build) o detiene Play (en el editor)
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicación compilada
        Application.Quit();

        // Si estás dentro del editor, detiene el modo "Play"
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
