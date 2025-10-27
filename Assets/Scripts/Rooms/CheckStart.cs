using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStart : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetTrigger("apprear");
        }    
    }
}
