using UnityEngine;

public class ControlvolumeAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.3f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            // Se ainda não houver AudioSource, emite um aviso
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource não encontrado. Certifique-se de que há um AudioSource neste objeto ou atribua um manualmente.");
                return;
            }
        }

        audioSource.volume = volume;
    }
}