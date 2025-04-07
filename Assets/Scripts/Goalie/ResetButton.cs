using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void ResetGane()
    {
        //this is only here as a quick way to test the toy cuz it is very easy to miss the puck and it's kindof a pain in the ass to keep reloading the scene - J
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
