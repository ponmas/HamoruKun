using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeTraining : MonoBehaviour
{
    public void returnmenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void reset()
    {
        SceneManager.LoadScene("ModeTraining");

    }

}