using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitChime : MonoBehaviour
{
    public float t;
    public GameObject chickenObject;
    IEnumerator chicken;
    public KitClock clock;

    private void Start()
    {
        clock.OnTheHour.AddListener(Chime);
    }
    public void Chime(int hour)
    {
        Debug.Log("Chiming ! " + hour + " o'clock !");
        StartCoroutine(CallChicken());
    }

    public void ChimeWithoutArguments()
    {
        Debug.Log("Chiming !");
    }
    private IEnumerator CallChicken()
    {
        chicken = (ShowChicken());
        yield return StartCoroutine(chicken);
    }
    private IEnumerator ShowChicken()
    {
        t = 0;
        while (t < clock.timeAnHourTakes)
        {
            t += Time.deltaTime;
            chickenObject.GetComponent<SpriteRenderer>().enabled = true;
            if(t > 0.5f)
            {
                chickenObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            yield return null;
        }
    }
}
