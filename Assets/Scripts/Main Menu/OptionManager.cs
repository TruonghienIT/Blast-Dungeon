using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public TMP_Dropdown graphics_Dropdown;
    public Slider masterVol, musicVol, sfxVol;
    private void Start()
    {
        if (SoundManager.instance == null) return;

        if (masterVol) masterVol.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        if (musicVol) musicVol.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        if (sfxVol) sfxVol.value = PlayerPrefs.GetFloat("SfxVolume", 0.75f);

        if (masterVol) masterVol.onValueChanged.AddListener(SoundManager.instance.ChangeMasterVolume);
        if (musicVol) musicVol.onValueChanged.AddListener(SoundManager.instance.ChangeMusicVolume);
        if (sfxVol) sfxVol.onValueChanged.AddListener(SoundManager.instance.ChangeSfxVolume);

        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(savedQuality);
        if (graphics_Dropdown) graphics_Dropdown.value = savedQuality;
    }
    public void ChangeGraphicsQuality()
    {
        if (graphics_Dropdown == null) return;

        int qualityLevel = graphics_Dropdown.value;
        QualitySettings.SetQualityLevel(qualityLevel);
        PlayerPrefs.SetInt("GraphicsQuality", qualityLevel);
    } 
}
