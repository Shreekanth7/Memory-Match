using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip flipClip;
    [SerializeField] private AudioClip matchClip;
    [SerializeField] private AudioClip mismatchClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip bgmClip;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        PlayBGM();
    }
    
    public void PlayBGM()
    {
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayFlip()
    {
        PlaySound(flipClip);
    }

    public void PlayMatch()
    {
        PlaySound(matchClip);
    }

    public void PlayMismatch()
    {
        PlaySound(mismatchClip);
    }

    public void PlayGameOver()
    {
        PlaySound(gameOverClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}