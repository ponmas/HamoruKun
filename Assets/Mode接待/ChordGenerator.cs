using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;// Needed for Math
using UnityEngine.UI;

public class ChordGenerator : MonoBehaviour
{
    public GameObject tone;
    public GameObject generate;

    const double PI = System.Math.PI;
    const double PI2 = PI * 2.0;
    const double PI_2 = PI / 2.0;
    const double sampling_frequency = 48000;
    const double PI2_SR = PI2 / sampling_frequency;

    double frequencyroot = 440;
    double frequency3rd = 440;         //鳴らす音
    double frequency5th = 440;         //鳴らす音

    public double gain = 0.05;
    private double incrementroot, increment3rd, increment5th;
    private double timeroot = 0, time3rd = 0, time5th = 0;
    private PlayTone playTone = PlayTone.Stop;

    private enum PlayTone
    {
        Stop,
        Go
        //C,Cs,D,Ds,E,F,Fs,G,Gs,A,As,B
    }

    // Update is called once per frame
    void Update()
    {
        float t1;
        t1 = tone.GetComponent<MicroFFT>().tone;

        if (t1 > 0)
        {
            Debug.Log("tone=" + t1);
            frequencyroot = 110.0f * Math.Pow(2, (t1) / 12.0f);
            frequency3rd = 110.0f * Math.Pow(2, (t1 + 4+12) / 12.0f);
            frequency5th = 110.0f * Math.Pow(2, (t1 + 7+12) / 12.0f);
            playTone = PlayTone.Go;
        }
        else
        {
            playTone = PlayTone.Stop;
        }

    }



    void SquareWave(float[] data, int channels)
    {
        incrementroot = frequencyroot * 2 * PI / sampling_frequency;
        increment3rd = frequency3rd * 2 * PI / sampling_frequency;
        increment5th = frequency5th * 2 * PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            timeroot = timeroot + incrementroot;
            time3rd = time3rd + increment3rd;
            time5th = time5th + increment5th;

            //data[i] = (float)(gain * Math.Sin(timeroot));

            //data[i] = (float)(gain * (((time3rd % PI2) < PI2 * 0.5 ? 1.0 : -1.0) + ((time5th % PI2) < PI2 * 0.5 ? 1.0 : -1.0)) * 0.5);

            //data[i] = (float)(gain * Math.Sin(time5th));
            //data[i] = (float)(gain * Math.Sin(time3rd));
            data[i] = (float)(gain * 0.5 * (Math.Sin(time3rd) + Math.Sin(time5th)));

            if (channels == 2)
                data[i + 1] = data[i];
            if (time3rd > 2 * Math.PI)
                time3rd = 0;
            if (time5th > 2 * Math.PI)
                time5th = 0;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        switch (playTone)
        {
            case PlayTone.Go:
                SquareWave(data, channels);
                break;
        }
    }

}
