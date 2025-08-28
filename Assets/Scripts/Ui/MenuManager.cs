using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip intereactSound;
    private int currentPosition;
    private void Awake()
    {
        ChangePosition(0);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }    
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            ChangePosition(+1);
        }    

        if(Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
        {
            Interact();
        }    
    }
    public void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }

        if (currentPosition < 0)
        {
            currentPosition = buttons.Length - 1;
        }
        else if (currentPosition > buttons.Length - 1)
        {
            currentPosition = 0;
        }

        AssignPossition();
    }
    private void AssignPossition()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(intereactSound);
        if (currentPosition == 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
        }
        else if (currentPosition == 1)
        {
            //Mở cài đặt
        }
        else if (currentPosition == 2)
        {
            //
        }
        else if (currentPosition == 3)
        {
            Application.Quit();
        }
    }
}
