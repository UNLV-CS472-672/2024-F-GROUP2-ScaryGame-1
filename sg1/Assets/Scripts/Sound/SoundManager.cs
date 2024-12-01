using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Background Music")]
    public AudioSource backgroundMusicSource; 
    public AudioClip mainMenuMusic; 
    public AudioClip endCreditsMusic; 

    [Header("Audio Sources")]
    public AudioSource saltShakerAudioSource;
    public AudioSource healthpackAudioSource;

    [Header("Audio Clips")]
    public AudioClip saltShakerClip;
    public AudioClip healthpackClip;

    [Header("Doorway Sounds")]
    public AudioSource doorOpenAudioSource;
    public AudioSource doorCloseAudioSource;

    [Header("Audio Clips")]
    public AudioClip doorOpenClip; 
    public AudioClip doorCloseClip; 


    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true; 
            backgroundMusicSource.playOnAwake = false; 
            backgroundMusicSource.spatialBlend = 0f; 
            backgroundMusicSource.volume = AudioSettingsManager.MasterVolume; 
        }

        if (saltShakerAudioSource == null)
        {
            saltShakerAudioSource = gameObject.AddComponent<AudioSource>();
            saltShakerAudioSource.clip = saltShakerClip;
            saltShakerAudioSource.playOnAwake = false;
        }

        if (healthpackAudioSource == null)
        {
            healthpackAudioSource = gameObject.AddComponent<AudioSource>();
            healthpackAudioSource.clip = healthpackClip;
            healthpackAudioSource.playOnAwake = false;
        }

        if (doorOpenAudioSource == null)
        {
            doorOpenAudioSource = gameObject.AddComponent<AudioSource>();
            doorOpenAudioSource.clip = doorOpenClip;
            doorOpenAudioSource.playOnAwake = false;
            doorOpenAudioSource.spatialBlend = 1.0f;
        }

        if (doorCloseAudioSource == null)
        {
            doorCloseAudioSource = gameObject.AddComponent<AudioSource>();
            doorCloseAudioSource.clip = doorCloseClip;
            doorCloseAudioSource.playOnAwake = false;
            doorCloseAudioSource.spatialBlend = 1.0f;
        }
    }

    private void Start()
    {
        UpdateVolume();
    }

    private void Update()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = AudioSettingsManager.MasterVolume;
        }
    }

    public void PlayMainMenuMusic()
    {
        if (backgroundMusicSource != null && mainMenuMusic != null)
        {
            backgroundMusicSource.clip = mainMenuMusic;
            backgroundMusicSource.volume = AudioSettingsManager.MasterVolume;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Main menu music is not set up properly.");
        }
    }

    public void PlayEndCreditsMusic()
    {
        if (backgroundMusicSource != null && endCreditsMusic != null)
        {
            backgroundMusicSource.clip = endCreditsMusic;
            backgroundMusicSource.volume = AudioSettingsManager.MasterVolume;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("End credits music is not set up properly.");
        }
    }

    public void StopMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void PlaySaltShakerSound()
    {
        saltShakerAudioSource.volume = AudioSettingsManager.MasterVolume;

        if (saltShakerAudioSource != null)
        {
            saltShakerAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Salt shaker Audio Source is not set up properly.");
        }
    }

    public void PlayHealthpackSound()
    {
        healthpackAudioSource.volume = AudioSettingsManager.MasterVolume;

        if (healthpackAudioSource != null)
        {
            healthpackAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Health pack Audio Source is not set up properly.");
        }
    }

    public void PlayDoorOpenSound(Vector3 position)
    {
        
        if (doorOpenAudioSource != null)
        {
            doorOpenAudioSource.transform.position = position;
            doorOpenAudioSource.volume = AudioSettingsManager.MasterVolume;
            doorOpenAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Door open Audio Source is not set up properly.");
        }
    }

    public void PlayDoorCloseSound(Vector3 position)
    {
        
        if (doorCloseAudioSource != null)
        {
            doorCloseAudioSource.transform.position = position; 
            doorCloseAudioSource.volume = AudioSettingsManager.MasterVolume;
            doorCloseAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Door close Audio Source is not set up properly.");
        }
    }


}
