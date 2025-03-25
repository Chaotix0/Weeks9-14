using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 2;
    Animator animator;
    SpriteRenderer sr;
    public bool canRun = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        sr.flipX = direction < 0;

        animator.SetFloat("speed", Mathf.Abs(direction));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if(canRun == true)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }
    }


    public void AttackHasFinished()
    {
        canRun = true;
    }
}
