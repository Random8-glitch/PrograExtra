using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] AudioClips;

    public AudioSource AudioSourceTemplate;
    private AudioSource[] audioSources = new AudioSource[32];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("No deberia haber mas de uno AudioSource");
        }

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = Instantiate(AudioSourceTemplate, transform);
        }
    }

    public void PlaySound(string clipName)
    {
        AudioSource audioSource = null;
        AudioClip audioClip = null;

        foreach(AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                audioSource = source;
                break;
            }
        }

        if(audioSource == null)
        {
            return;
        }

        foreach(AudioClip clip in AudioClips)
        {
            if(clip.name == clipName)
            {
                audioClip = clip;
                break;
            }
        }

        if (audioClip == null)
        {
            Debug.LogError($"No se encontro: {clipName}");
            return;
        }

        audioSource.clip = audioClip;
        audioSource.volume = 0.15f;
        audioSource.Play();
    }
}
