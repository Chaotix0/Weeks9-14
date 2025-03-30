using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    public float speed = 3; //Goalie speed
    public float maxXPosition = 2f; // Right boundary
    public float minXPosition = -2f; // Left boundary
    Animator animator;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        pos.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.position = pos;

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    transform.Translate(-1, 0, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    transform.Translate(1, 0, 0);
        //}
    }
}
