using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    public float speed = 3; //Goalie speed
    public float maxXPosition = 2; // Right boundary
    public float minXPosition = -2; // Left boundary

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        pos.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minXPosition, maxXPosition);
        

        transform.position = pos;

        //animations for the differrent blocks
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Glove");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Butterfly");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Blocker");
        }
    }
}
