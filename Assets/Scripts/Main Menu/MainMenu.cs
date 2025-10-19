using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject levelPanel;
    public void PlayGame()
    {
        levelPanel.SetActive(true);
        mainMenu.SetActive(false);
    }    
    public void Option()
    {
        mainMenu.SetActive(false);
        optionPanel.SetActive(true);
    }    
    public void Cancel()
    {
        mainMenu.SetActive(true);
        optionPanel.SetActive(false);
        levelPanel.SetActive(false);
    }    
    public void QuitGame()
    {
        Application.Quit();
    }    
}
