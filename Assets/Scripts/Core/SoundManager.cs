using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;
    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }    
        //Xóa trùng lặp
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        ChangeMusicVolume(0);
        ChangSoundVolume(0);
    }
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot( _sound );
    }    
    public void ChangSoundVolume(float _change)
    {
        ChangSourceVolume(1, "soundVolume", _change, soundSource);
    }
    private void ChangSourceVolume(float baseVolume, string volumeName, float change, AudioSource audioSource)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }    
        float finalVolume = currentVolume * baseVolume;
        audioSource.volume = finalVolume;

        PlayerPrefs.SetFloat (volumeName, currentVolume);           
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }    
}   
