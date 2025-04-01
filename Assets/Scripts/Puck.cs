using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public float speed = 7; // puck speed
    public float maxXPosition = 2; // Right boundary
    public float minXPosition = -2; // Left boundary
    public float maxYPosition = 0; // Right boundary
    public float minYPosition = -2; // Left boundary
    public AnimationCurve size; //puck size
    float t;
    public float XValue;
    public float YValue;
    public int net = 0;
    public GameObject goalie;
    public GameObject puck;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void PuckSpawn()
    {
        if (transform.localScale.x <= 0)
        {
            Instantiate(puck);
            Destroy(gameObject); // Detroys puck when done
        }

    }
}
