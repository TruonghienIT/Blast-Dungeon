using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            SceneController.instance.NextLevel();
        }    
    }
        
    void UnlockNewLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel",1);
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
            Debug.Log("Unlocked Level: " + (currentLevel + 1));
        }
    }    
}
