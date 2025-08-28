using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;
    private TMP_Text txt;
    private void Awake()
    {
        txt = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volumeValue.ToString();
    }    
}
