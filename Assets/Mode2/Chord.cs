using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    public GameObject tone1;
    public GameObject tone2;
    public GameObject judge;



    // Update is called once per frame
    void Update()
    {
        float sa = 0, abssa = 0;
        float t1, t2;
        t1 = tone1.GetComponent<MicroFFT>().tone;
        t2 = tone2.GetComponent<MicroFFTmic2>().tone;

        //while (t1 > 12) t1 -= 12.0f;
        //while (t2 > 12) t2 -= 12.0f;

       // t1 = 3;

        sa = t1 - t2;
        abssa = (sa > 0) ? sa : -sa;

        while (abssa > 12) abssa -= 12.0f;
        while (abssa < -12) abssa += 12.0f;

        if (abssa > 6.3 && abssa < 7.7)
        {
            Debug.Log("5th!!!!! : " + sa + "  ,A:" + t1 + " ,B:" + t2);
            judge.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (abssa > 3.3 && abssa < 4.7)
        {
            Debug.Log("3rd!!!!! : " + sa + "  ,A:" + t1 + " ,B:" + t2);
            judge.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            Debug.Log("miss : " + sa);
            judge.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
