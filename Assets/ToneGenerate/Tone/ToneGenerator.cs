using UnityEngine;
using System;// Needed for Math
using UnityEngine.UI;

public class ToneGenerator : MonoBehaviour
{
    const double PI = System.Math.PI;
    const double PI2 = PI * 2.0;
    const double PI_2 = PI / 2.0;
    const double sampling_frequency = 48000;
    const double PI2_SR = PI2 / sampling_frequency;
    private enum PlayState
    {
        Stop,
        SineWave,
        SquareWave,
        TriangleWave,
        SawtoothWave
    }
    private enum PlayTone
    {
        Stop,
        Go
        //C,Cs,D,Ds,E,F,Fs,G,Gs,A,As,B
    }


    double frequency = 440;
    public double gain = 0.05;
    private double increment;
    private double time;
    private PlayState playState = PlayState.Stop;
    private PlayTone playTone = PlayTone.Stop;

    void SineWave(float[] data, int channels)
    {
        increment = frequency * 2 * PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            time = time + increment;

            data[i] = (float)(gain * Math.Sin(time));
            if (channels == 2)
                data[i + 1] = data[i];
            if (time > 2 * Math.PI)
                time = 0;
        }
    }

    void SquareWave(float[] data, int channels)
    {
        increment = frequency * 2 * PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            time = time + increment;

            data[i] = (float)(gain * ((time % PI2) < PI2 * 0.5 ? 1.0 : -1.0));
            if (channels == 2)
                data[i + 1] = data[i];
            if (time > 2 * Math.PI)
                time = 0;
        }
    }


    public void ButtonC()
    {
        frequency = 523.2511306011;
        playTone = PlayTone.Go;
    }

    public void ButtonCs()
    {
        frequency = 554.3652619537;
        playTone = PlayTone.Go;
    }
    public void ButtonD()
    {
        frequency = 587.3295358348;
        playTone = PlayTone.Go;
    }
    public void ButtonDs()
    {
        frequency = 622.2539674441;
        playTone = PlayTone.Go;
    }
    public void ButtonE()
    {
        frequency = 659.2551138257;
        playTone = PlayTone.Go;
    }
    public void ButtonF()
    {
        frequency = 698.4564628660;
        playTone = PlayTone.Go;
    }
    public void ButtonFs()
    {
        frequency = 739.9888454232;
        playTone = PlayTone.Go;
    }
    public void ButtonG()
    {
        frequency = 783.9908719634;
        playTone = PlayTone.Go;
    }
    public void ButtonGs()
    {
        frequency = 830.6093951598;
        playTone = PlayTone.Go;
    }
    public void ButtonA()
    {
        frequency = 880.0000000000;
        playTone = PlayTone.Go;
    }
    public void ButtonAs()
    {
        frequency = 932.3275230361;
        playTone = PlayTone.Go;
    }
    public void ButtonB()
    {
        frequency = 987.7666025122;
        playTone = PlayTone.Go;
    }
    public void ButtonUP()
    {
        frequency = 440;
        playTone = PlayTone.Stop;
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