using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public TMP_Text ControlsText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void save()
    {
            ControlsText.text = "Nice Save!";
    }
}
