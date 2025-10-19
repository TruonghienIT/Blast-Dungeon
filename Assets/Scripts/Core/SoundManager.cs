using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Mixer")]
    public AudioMixer mainAudioMixer;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    [Header("UI Sliders (Optional)")]
    public Slider masterVol;
    public Slider musicVol;
    public Slider sfxVol;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (musicSource == null && transform.childCount > 0)
            musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
            if (sfxSource == null)
                sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        int graphicsQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(graphicsQuality);

        float masterValue = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float musicValue = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxValue = PlayerPrefs.GetFloat("SfxVolume", 0.75f);

        SetMixerVolume("MasterVol", masterValue);
        SetMixerVolume("MusicVol", musicValue);
        SetMixerVolume("SfxVol", sfxValue);

        SyncSlidersWithPrefs();

        if (masterVol) masterVol.onValueChanged.AddListener(ChangeMasterVolume);
        if (musicVol) musicVol.onValueChanged.AddListener(ChangeMusicVolume);
        if (sfxVol) sfxVol.onValueChanged.AddListener(ChangeSfxVolume);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            PlayMusic(levelMusic);
        }
    }

    // ---- Xử lý Volume ----
    private void SyncSlidersWithPrefs()
    {
        if (masterVol) masterVol.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        if (musicVol) musicVol.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        if (sfxVol) sfxVol.value = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
    }

    private void SetMixerVolume(string parameterName, float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.001f, 1f)) * 20f;
        mainAudioMixer.SetFloat(parameterName, dB);
    }

    public void ChangeMasterVolume(float value)
    {
        SetMixerVolume("MasterVol", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        SetMixerVolume("MusicVol", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeSfxVolume(float value)
    {
        SetMixerVolume("SfxVol", value);
        PlayerPrefs.SetFloat("SfxVolume", value);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}
