using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;// Needed for Math
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    public GameObject tone;
    public GameObject judge;
    public GameObject standardline1;
    public GameObject standardline2;
    public GameObject standardline3;

    const double PI = System.Math.PI;
    const double PI2 = PI * 2.0;
    const double PI_2 = PI / 2.0;
    const double sampling_frequency = 48000;
    const double PI2_SR = PI2 / sampling_frequency;

    /* 音作り系 */
    double frequencyroot = 440;
    double frequency3rd = 440;
    double frequency5th = 440;
    int rand = 0;
    float rand_b = 0;
    public Text scaText;

    public double gain = 0.05;
    private double incrementroot, increment3rd, increment5th;
    private double timeroot = 0, time3rd = 0, time5th = 0;
    private PlayTone playTone = PlayTone.Stop;

    private float timeleft = 0;     //時間の変数
    private int randcount = 0;      //

    /* 点数系 */
    public int score_num = 0;      //点数の変数
    public Text score_text;

    float tolerate = 0.5f;          //音の許容範囲

    private enum PlayTone
    {
        Stop,
        Go
        //C,Cs,D,Ds,E,F,Fs,G,Gs,A,As,B
    }

    private string[] noteNames = { "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯" };

    // Update is called once per frame

    private void Start()
    {
        judge.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        float sa = 0, abssa = 0;
        float tone_in;


        //だいたい1秒ごとに処理を行う
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            /* 次の音に変わるまでの時間 */
            timeleft = 7.0f;

            rand_b = rand;
            rand = (int)(UnityEngine.Random.Range(0, 12.0f)) % 12;

            /* 半音差の変化だとむつかしいかもしれないので全音以上の変化にした */
            while (rand - rand_b >= -1&&rand - rand_b <= 1)
            {
                rand = (int)(UnityEngine.Random.Range(0, 12.0f)) % 12;
            }

            switch (rand)
            {
                case 3:
                    //frequencyroot = 130.8127826502f;
                    //frequencyroot = 261.6255653005;
                    frequencyroot = 523.2511306011f;        //C4
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 4:
                    //frequencyroot = 138.5913154884f;
                    //frequencyroot = 277.1826309768f;
                    frequencyroot = 554.3652619537f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 5:
                    //frequencyroot = 146.8323839587f;
                    //frequencyroot = 293.6647679174f;
                    frequencyroot = 587.3295358348f;        //D4
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 6:
                    //frequencyroot = 155.5634918610f;
                    //frequencyroot = 311.1269837220f;
                    frequencyroot = 622.2539674441f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 7:
                    //frequencyroot = 164.8137784564f;
                    //frequencyroot = 329.6275569128f;
                    frequencyroot = 659.2551138257f;        //E4
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 8:
                    //frequencyroot = 174.6141157165f;
                    //frequencyroot = 349.2282314330f;
                    frequencyroot = 698.4564628660f;        //F4
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 9:
                    //frequencyroot = 184.9972113558f;
                    //frequencyroot = 369.9944227116f;
                    frequencyroot = 739.9888454232f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 10:
                    //frequencyroot = 195.9977179908f;
                    frequencyroot = 391.9954359817f;        //G3
                    //frequencyroot = 783.9908719634f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 11:
                    //frequencyroot = 207.6523487899f;
                    frequencyroot = 415.3046975799f;
                    //frequencyroot = 830.6093951598f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;
                case 0:
                    //frequencyroot = 220.0000000000;
                    frequencyroot = 440.0000000000f;        //A4
                    //frequencyroot = 880.0000000000f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 1:
                    //frequencyroot = 233.0818807590f;
                    frequencyroot = 466.1637615180f;
                    //frequencyroot = 932.3275230361f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 2:
                    //frequencyroot = 246.9416506280f;
                    frequencyroot = 493.8833012561f;        //B4
                    //frequencyroot = 987.7666025122f;
                    scaText.text = noteNames[rand];
                    playTone = PlayTone.Go;
                    break;

                case 12:
                    frequencyroot = 440;
                    playTone = PlayTone.Stop;
                    break;
            }

            scaText.text = "基準:" + scaText.text;

            standardline1.transform.position = new Vector3(0, -80.0f + (rand - 12) * 4.0f, 0);
            standardline2.transform.position = new Vector3(0, -80.0f + rand * 4.0f, 0);
            standardline3.transform.position = new Vector3(0, -80.0f + (rand + 12) * 4.0f, 0);

            randcount += 1;
        }


        tone_in = tone.GetComponent<MicroFFT>().tone;

        sa = tone_in - rand;
        abssa = sa;

        while (abssa > 12) abssa -= 12.0f;
        while (abssa < 0) abssa += 12.0f;

        if (tone_in == -1)
        {
            judge.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (abssa < tolerate || abssa > 12 - tolerate)
        {
            Debug.Log("octave : " + sa + "  ,In:" + tone_in + " ,Root:" + rand);
            judge.GetComponent<Renderer>().material.color = Color.cyan;
            if (randcount <= 5)
            {
                //score_num += 1;
                /* 被験者実験の評価のためにユニゾンは除くことにする。 */
            }
        }
        else if (abssa > 6 - tolerate && abssa < 7 + tolerate)
        {
            Debug.Log("5th!!! : " + sa + "  ,In:" + tone_in + " ,Root:" + rand);
            judge.GetComponent<Renderer>().material.color = Color.green;
            if (randcount <= 5)
            {
                score_num += 3;
            }
        }
        else if (abssa > 3 - tolerate && abssa < 4 + tolerate)
        {
            Debug.Log("3rd!!! : " + sa + "  ,In:" + tone_in + " ,Root:" + rand);
            judge.GetComponent<Renderer>().material.color = Color.yellow;
            if (randcount <= 5)
            {
                score_num += 3;
            }
        }
        else
        {
            Debug.Log("miss : " + sa + "Root: " + rand);
            judge.GetComponent<Renderer>().material.color = Color.red;
        }

        score_text.text = "Score:" + score_num.ToString("D5");
    }


    void SquareWave(float[] data, int channels)
    {
        incrementroot = frequencyroot * 2 * PI / sampling_frequency;
//        increment3rd = frequency3rd * 2 * PI / sampling_frequency;
//        increment5th = frequency5th * 2 * PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            timeroot = timeroot + incrementroot;
            time3rd = time3rd + increment3rd;
            time5th = time5th + increment5th;

            //data[i] = (float)(gain * Math.Sin(timeroot));

            //data[i] = (float)(gain * (((time3rd % PI2) < PI2 * 0.5 ? 1.0 : -1.0) + ((time5th % PI2) < PI2 * 0.5 ? 1.0 : -1.0)) * 0.5);

            //data[i] = (float)(gain * Math.Sin(time5th));
            //data[i] = (float)(gain * Math.Sin(time3rd));
            data[i] = (float)(gain * Math.Sin(timeroot));

            if (channels == 2)
                data[i + 1] = data[i];
            /*           if (time3rd > 2 * Math.PI)
                           time3rd = 0;
                       if (time5th > 2 * Math.PI)
                           time5th = 0;
                       */
            if (timeroot > 2 * Math.PI)
                timeroot = 0;
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
