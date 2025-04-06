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
    public int num = 0;
    float t;
    public float XValue;
    public float YValue;
    public GameObject goalie;
    public GameObject puck;
    public GameObject score;
    public GameObject TextControls;
    public GameObject CrazyPuck;
    public UnityEvent SAVE; // adding to the score when save the puck
    // Start is called before the first frame update
    void Start()
    {
        XValue = Random.Range(minXPosition, maxXPosition); //random X value
        YValue = Random.Range(minYPosition, maxYPosition); //random Y value

        transform.position = new Vector3(XValue, YValue); //random puck spots
        
        SAVE = new UnityEvent();
        SAVE.AddListener(score.GetComponent<Score>().save);
        SAVE.AddListener(TextControls.GetComponent<Controls>().save);
        num = Random.Range(0, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if (num != 10) //call Crazy puck
        {
            t += Time.deltaTime; //makes sure the puck scales every frame evenly
            transform.localScale = Vector2.one * size.Evaluate(t); //scales down puck size
            PuckEnd(); //spawns puck eqach frame
        }
        if (num == 10) // call Crazy puck
        {
            StartCoroutine(CrazyPuck.GetComponent<Puck2>().Crazy()); //start CrazyPuck

            t += Time.deltaTime; //makes sure the puck scales every frame evenly
            transform.localScale = Vector2.one * size.Evaluate(t); //scales down puck size
            PuckEnd(); //spawns puck eqach frame
        }
        
    }

    void PuckEnd()
    {
        if (transform.localScale.x <= 0.001f)
        {
            if (goalie.transform.position.x >= puck.transform.position.x - 1.2f && goalie.transform.position.x <= puck.transform.position.x + 1.2f)// checking x hitboxes for goalie
            {
                if (goalie.transform.position.y >= puck.transform.position.y - 1 && goalie.transform.position.y <= puck.transform.position.y + 1)// checking y hitboxes for goalie
                {
                    Instantiate(puck); //makes duplicate puck
                    SAVE.Invoke();
                }
                else 
                {
                    SAVE.RemoveListener(score.GetComponent<Score>().save);
                }
            }
            Destroy(gameObject); // Detroys puck when done

        }
    }
}
