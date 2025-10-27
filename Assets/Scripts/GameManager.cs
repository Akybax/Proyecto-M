using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void PlayerDefeated(GameObject defeatedPlayer)
{
    Debug.Log("Jugador derrotado: " + defeatedPlayer.name);
    Invoke("RestartRound", 2f);
}

void RestartRound()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

}
