using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        audioSource.volume = PlayerPrefs.GetFloat("audio_volume", 1);
    }

    public void PlayClip(AudioClip clip, bool isLoop = false)
    {
        if (isLoop) audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
