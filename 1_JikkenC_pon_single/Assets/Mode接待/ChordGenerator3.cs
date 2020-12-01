using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChordGenerator3 : MonoBehaviour
{
    // 波形を描画する
//    public LineRenderer line;

    // マイクからの音を拾う
    public AudioSource mic;
    public AudioSource mic1;
    public AudioSource mic2;
    //private string mic_name= "マイク (Realtek High Definition Audio)";
    //private string mic_name = "マイク (WO Mic Device)";
    //private string mic_name = "マイク (USB PnP Sound Device)";
    private string mic_name = null;

    // 波形描画のための変数
 /*   private float[] wave;
    private int wave_num;
    private int wave_count;
    */


    public Text scaText;
    public float tone = 0;

    void Start()
    {

        scaText.text = "No input";//テキストの変更

        // 波形描画のための変数の初期化
/*        wave_num = 300;
        wave = new float[wave_num];
        wave_count = 0;
*/
        /*    foreach (string device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
                mic_name = device;
            }*/

        // micにマイクを割り当てる
        mic.clip = Microphone.Start(mic_name, true, 999, 44100);
        mic1.clip = mic.clip;
        mic2.clip = mic.clip;


        //上のサンプリングレートを変えたらエラーがなくなった???

        if (mic.clip == null)
        {
            Debug.LogError("Microphone.Start");
        }
        mic.loop = true;
        mic.mute = false;
        mic1.loop = true;
        mic1.mute = false;
        mic2.loop = true;
        mic2.mute = false;

        // 録音の準備が出来るまで待つ
        while (!(Microphone.GetPosition(mic_name) > 0)) { }
        mic.Play();
        mic1.Play();
        mic2.Play();
    }

    void Update()
    {
        // 諸々の解析
        float hertz = SoundLibrary.AnalyzeSound(mic, 1024 * 8, 0.01f);

        if (hertz > 0)
        {
            float scale = SoundLibrary.ConvertHertzToScale(hertz);
            string s = SoundLibrary.ConvertScaleToString(scale);
            Debug.Log("Mic.1 : " + hertz + "Hz, Scale:" + scale + ", " + s);
            scaText.text = s;
            // 波形描画
//            wave[wave_count] = scale;
            tone = scale;
        }
        else
        {
//            wave[wave_count] = wave[(wave_count + wave.Length - 1) % wave.Length];
            scaText.text = "No input";
            tone = -1;
        }
 //       SoundLibrary.ScaleWave(wave, wave_count, line);
 //       wave_count++;
 //       if (wave_count >= wave_num) wave_count = 0;
    }
}