using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockHand : MonoBehaviour
{

    public UnityEvent TimerHasFinshed;
    public float timerLength = 2;
    public float t;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -15 * Time.deltaTime);
        t += Time.deltaTime;
        if (t > timerLength)
        {
            t = 0;
            TimerHasFinshed.Invoke();
        }
    }

    public void kookoo()
    {
        Debug.Log("Button pushed");
    }
}
