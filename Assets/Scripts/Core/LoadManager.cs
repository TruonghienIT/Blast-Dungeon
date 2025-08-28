using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }    
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }    
    }
    public void LoadCurrentLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        SceneManager.LoadScene(currentLevel);
    }  
    public void Restart()
    {
        //SceneManager.LoadScene(currentLevel);
    }    
}
