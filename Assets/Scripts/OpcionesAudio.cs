using UnityEngine;

public class OpcionesAudio : MonoBehaviour
{
    public void ToggleMusica()
    {
        if (GameManager.Instance != null)
        {
            AudioSource musica = GameManager.Instance.GetComponent<AudioSource>();
            if (musica != null)
            {
                musica.mute = !musica.mute;
                Debug.Log("Música mute: " + musica.mute);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el GameManager para controlar la música.");
        }
    }

    public void ToggleSFX()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.ToggleSFX();
            Debug.Log("SFX mute cambiado");
        }
        else
        {
            Debug.LogWarning("No se encontró el SFXManager para controlar los efectos.");
        }
    }
}

