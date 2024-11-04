using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    public AudioClip backgroundMusic; // Background music clip
    public AudioClip mergeSound;       // Sound effect for merging slimes
    public AudioClip dropSound; // Sound effect for dropping slimes
    private AudioSource musicSource;   // AudioSource for background music
    private AudioSource soundSource;    // AudioSource for sound effects

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Set up audio sources
        musicSource = gameObject.AddComponent<AudioSource>();
        soundSource = gameObject.AddComponent<AudioSource>();

        // Configure music source
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.06f; // Adjust volume as needed
        musicSource.Play(); // Start playing background music
    }

    public void PlayDropSound()
    {
        soundSource.PlayOneShot(dropSound); // Play the drop sound effect
        soundSource.volume = 0.1f; // Adjust volume for sound effects
    }

    public void PlayMergeSound()
    {
        soundSource.PlayOneShot(mergeSound); // Play the merge sound effect
    }
}
