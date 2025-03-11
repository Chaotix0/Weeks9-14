using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent TimerHasFinshed;
    public float timerLength = 2;
    public float t;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength) 
        {
            t = 0;
            TimerHasFinshed.Invoke();
        }
    }


    public void IJustPushedTheButton()
    {
        Debug.Log("Button pushed");
    }

    public void IAlsoPushedTheButton()
    {
        Debug.Log("i also did");
    }

    public void MouseIsOverTheImage() 
    {
        Debug.Log("mouse is over the sprite");
        banana.localScale = Vector3.one * 1.2f;
    }

    public void MouseIsNotOverTheImage()
    {
        Debug.Log("left the sprite");
        banana.localScale = Vector3.one;
    }
}
