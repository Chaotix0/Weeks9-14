using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public float maxXPosition = 2; // Right boundary
    public float minXPosition = -2; // Left boundary
    public float maxYPosition = 0; // Right boundary
    public float minYPosition = -2; // Left boundary
    public AnimationCurve size; //puck size
    float t;
    public float XValue;
    public float YValue;
    public GameObject goalie;
    public GameObject puck;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Save());
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        XValue = Random.Range(minXPosition, maxXPosition); //random X value
        YValue = Random.Range(minYPosition, maxYPosition); //random Y value
        transform.localScale = Vector2.one * size.Evaluate(t); //scales down puck size
        PuckSpawn(); //spawns puck eqach frame
    }

    //IEnumerator Save()
    //{
    //    while () 
    //    {
    //
    //    }
    //}

    void PuckSpawn()
    {
        if (transform.localScale.x <= 0.001f)
        {
            transform.position = new Vector3(XValue, YValue); //random puck spots

            Instantiate(puck); //makes duplicate puck
            Destroy(gameObject); // Detroys puck when done
        }

    }
}
