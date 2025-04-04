using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

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
    public GameObject score;
    public UnityEvent SAVE; // adding to the score when save the puck
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Save());
        XValue = Random.Range(minXPosition, maxXPosition); //random X value
        YValue = Random.Range(minYPosition, maxYPosition); //random Y value

        transform.position = new Vector3(XValue, YValue); //random puck spots
        
        SAVE = new UnityEvent();
        SAVE.AddListener(score.GetComponent<Score>().save);

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime; //makes sure the puck scales every frame evenly
        transform.localScale = Vector2.one * size.Evaluate(t); //scales down puck size
        PuckEnd(); //spawns puck eqach frame
    }

    void PuckEnd()
    {
        if (transform.localScale.x <= 0.001f)
        {
            if (goalie.transform.position.x >= puck.transform.position.x - 1.2f && goalie.transform.position.x <= puck.transform.position.x + 1.2f)// checking hitboxes for goalie
            {
                Instantiate(puck); //makes duplicate puck
                SAVE.Invoke();
            }
            Destroy(gameObject); // Detroys puck when done

        }
    }
}
