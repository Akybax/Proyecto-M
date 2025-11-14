using UnityEngine;

public class OpcionesAudio : MonoBehaviour
{
    public Animator personajeAnimator; 
    public string animacionTrigger = "Reaccion";

    void Start()
{
    if (personajeAnimator != null)
    {
        personajeAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}

    public void ToggleMusica()
    {
        if (GameManager.Instance != null)
        {
            AudioSource musica = GameManager.Instance.GetComponent<AudioSource>();
            if (musica != null)
            {
                musica.mute = !musica.mute;
                Debug.Log("Música mute: " + musica.mute);
                
            if (personajeAnimator != null)
                {
                    personajeAnimator.SetTrigger(animacionTrigger);
                }   

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

