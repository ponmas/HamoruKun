using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    public void buttonMode1()
    { 
        SceneManager.LoadScene("ModeEntertain");

    }
    public void buttonMode2()
    { 
        SceneManager.LoadScene("ModeTraining");

    }
    public void buttonMode3()
    { 
        SceneManager.LoadScene("Mode2_SomeIn");

    }

}