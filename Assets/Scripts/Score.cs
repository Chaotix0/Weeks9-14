using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    float score = 0;
    TextMeshPro ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void save()
    {
        ScoreText.text = score.ToString();
        score += 1;
    }
}
