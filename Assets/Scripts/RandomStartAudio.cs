using UnityEngine;

public class RandomStartAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume = 0.3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudioFromRandomPoint();
    }

    void PlayAudioFromRandomPoint()
    {
        float audioDuration = audioSource.clip.length;
        float randomStartTime = Random.Range(1f, audioDuration);
        audioSource.time = randomStartTime;
        audioSource.Play();
        audioSource.volume = volume;
    }
}