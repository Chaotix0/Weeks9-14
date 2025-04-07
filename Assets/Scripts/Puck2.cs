/*
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Puck2 : MonoBehaviour
{
    public float maxXPosition = 2; // Right boundary
    public float minXPosition = -2; // Left boundary
    public float maxYPosition = 0; // Right boundary
    public float minYPosition = -2; // Left boundary
    public AnimationCurve size; //puck size
    public AnimationCurve crazy; //CrazyPuck movement
    float t;
    public float XValue;
    public float YValue;
    public GameObject goalie;
    public GameObject puck;
    public GameObject CrazyPuck;
    public GameObject score;
    public UnityEvent SAVE; // adding to the score when save the puck
    public IEnumerator CRAZY;
    // Start is called before the first frame update
    void Start()
    {
        XValue = Random.Range(minXPosition, maxXPosition); //random X value
        YValue = Random.Range(minYPosition, maxYPosition); //random Y value

        transform.position = new Vector3(XValue, YValue); //random puck spots

        SAVE = new UnityEvent();
        SAVE.AddListener(score.GetComponent<Score>().save);

        CRAZY = Crazy();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime; //makes sure the puck scales every frame evenly
        transform.localScale = Vector2.one * size.Evaluate(t); //scales down puck size
        transform.position = Vector3.Lerp(new Vector3(XValue - 1, YValue), new Vector3(XValue, YValue), crazy.Evaluate(t)); //Crazy puck movement
    }
    public IEnumerator Crazy()
    {
        while (puck.GetComponent<Puck>().num == 10) 
        {
            if(CrazyPuck != null)
            {
                PuckEnd(); //spawns Crazy puck
            }
            yield return null;
        }
    }
    

    void PuckEnd()
    {
        if (transform.localScale.x <= 0.001f)
        {
            if (goalie.transform.position.x >= CrazyPuck.transform.position.x - 1.2f && goalie.transform.position.x <= CrazyPuck.transform.position.x + 1.2f)// checking hitboxes for goalie
            {
                if (goalie.transform.position.y >= CrazyPuck.transform.position.y - 1 && goalie.transform.position.y <= CrazyPuck.transform.position.y + 1)// checking y hitboxes for goalie
                {
                    Instantiate(CrazyPuck); //makes duplicate Crazy puck
                    SAVE.Invoke();
                }
            }
            Destroy(gameObject); // Detroys puck when done
            CrazyPuck = null;
        }
    }
}
*/