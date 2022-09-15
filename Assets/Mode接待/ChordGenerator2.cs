using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChordGenerator2 : MonoBehaviour
{

    // マイクからの音を拾う
    public AudioSource mic1;
    public AudioSource mic2;
    public AudioSource mic3;
    //private string mic_name= "マイク (Realtek High Definition Audio)";
    //private string mic_name = "マイク (WO Mic Device)";
    //private string mic_name = "マイク (USB PnP Sound Device)";
    private string mic_name = null;


    void Start()
    {

        // micにマイクを割り当てる
        mic1.clip = Microphone.Start(mic_name, true, 999, 44100);
        mic2.clip = mic1.clip;
        mic3.clip = mic1.clip;


        //上のサンプリングレートを変えたらエラーがなくなった???

        if (mic1.clip == null)
        {
            Debug.LogError("Microphone.Start");
        }

        mic1.loop = true;
        mic1.mute = false;
        mic2.loop = true;
        mic2.mute = false;
        mic3.loop = true;
        mic3.mute = false;

        // 録音の準備が出来るまで待つ
        while (!(Microphone.GetPosition(mic_name) > 0)) { }
        mic1.Play();
        mic2.Play();
        mic3.Play();

    }

    void Update()
    {

    }
}