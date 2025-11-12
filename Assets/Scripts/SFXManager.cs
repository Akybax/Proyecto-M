using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Efectos de Sonido")]
    public AudioSource audioSource;
    public AudioClip golpeClip;
    public AudioClip dañoClip;
    public AudioClip finRondaClip;
    public AudioClip ParryClip;

    private bool sfxMuted = false;

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

    public void PlayGolpe()
    {
        PlaySound(golpeClip);
    }

    public void PlayDaño()
    {
        PlaySound(dañoClip);
    }

    public void PlayFinRonda()
    {
        PlaySound(finRondaClip);
    }

     public void PlayParry()
    {
        PlaySound(ParryClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (!sfxMuted && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleSFX()
    {
        sfxMuted = !sfxMuted;
        Debug.Log("SFX mute: " + sfxMuted);
    }

    public bool IsMuted()
    {
        return sfxMuted;
    }
}
