using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckPoint;
    private Health playerHeath;
    private UiManager uiManager;
    private void Awake()
    {
        playerHeath = GetComponent<Health>();
        uiManager = FindObjectOfType<UiManager>();
    }
    public void CheckRespawn()
    {
        if(currentCheckPoint == null)
        {
            uiManager.GameOver();

            return;
        }    
        transform.position = currentCheckPoint.position;
        playerHeath.Respawn();
        
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckPoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckPoint = collision.transform;
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("apprear");
        }    
    }
}
