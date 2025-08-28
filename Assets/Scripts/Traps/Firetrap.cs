using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage; 

    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Health playerHealth;

    private bool triggered;
    private bool active;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if(!triggered) //nếu bẫy chưa kích hoạt
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active) //bẫy đã kích hoạt
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }    
    }
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }    
}
