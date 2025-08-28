using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));
    }    
    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }    
        }    
        return 0;
    }    
}
