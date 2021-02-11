using UnityEngine;
using System.Collections;

public class MicroFFT2 : MonoBehaviour
{
    // 波形を描画する
    public LineRenderer line;

    // マイクからの音を拾う
    public AudioSource[] mic;
    private string[] mic_name;// = { "マイク (Realtek High Definition Audio)", "マイク (WO Mic Device)" };

    // 波形描画のための変数
    private float[] wave;
    private int wave_num;
    private int wave_count;


    void Start()
    {
        for (int micnum = 0; micnum < mic_name.Length; micnum++)
        {
            // 波形描画のための変数の初期化
            wave_num = 300;
            wave = new float[wave_num];
            wave_count = 0;

            /*    foreach (string device in Microphone.devices)
                {
                    Debug.Log("Name: " + device);
                    mic_name = device;
                }*/

            // micにマイクを割り当てる
            mic[micnum].clip = Microphone.Start(mic_name[micnum], true, 999, 44100);


            //上のサンプリングレートを変えたらエラーがなくなった???

            if (mic[micnum].clip == null)
            {
                Debug.LogError("Microphone.Start");
            }
            mic[micnum].loop = true;
            mic[micnum].mute = false;

            // 録音の準備が出来るまで待つ
            while (!(Microphone.GetPosition(mic_name[micnum]) > 0)) { }
            mic[micnum].Play();
        }
    }

    void Update()
    {
        // 諸々の解析
        float hertz = SoundLibrary.AnalyzeSound(mic[1], 1024 * 8, 0.04f);

        if (hertz > 0)
        {
            float scale = SoundLibrary.ConvertHertzToScale(hertz);
            string s = SoundLibrary.ConvertScaleToString(scale);
            Debug.Log(hertz + "Hz, Scale:" + scale + ", " + s);
            // 波形描画
            wave[wave_count] = scale;
        }
        else if (wave_count != 0)
        {
            wave[wave_count] = wave[wave_count - 1];
            Debug.Log("No input.");
        }
        SoundLibrary.ScaleWave(wave, wave_count, line);
        wave_count++;
        if (wave_count >= wave_num) wave_count = 0;
    }
}