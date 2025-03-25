using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 2;
    Animator animator;
    SpriteRenderer sr;
    public bool canRun = true;
    public AudioSource Step;
    public bool AudioStep = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Step = GetComponent<AudioSource>();
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
    public void Footsteps()
    {
        Step.Play();
    }
}
