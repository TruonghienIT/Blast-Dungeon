using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHeath;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float invincibilityDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Hp Bar")]
    [SerializeField] private Image hpBar;
    private void Awake()
    {   
        currentHealth = startingHeath;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        UpdateHpbar();
    }
    private void Update()
    {
        if(transform.position.y <= -7.2f)
        {
            DieByFalling();
        }    
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHeath);
        UpdateHpbar();
        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invincibility());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("die");
                Rigidbody2D  rb = GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                }    
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    private void DieByFalling()
    {
        TakeDamage(currentHealth);
    }    
    public void AddHealth(float _healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healthValue, 0, startingHeath);
    }
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHeath);
        animator.ResetTrigger("die");
        animator.Play("PlayerIdle");
        StartCoroutine(Invincibility());
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }    
    private IEnumerator Invincibility()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invincibilityDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invincibilityDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;   
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }    
    private void UpdateHpbar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHealth/startingHeath;
        }
    }    
}
