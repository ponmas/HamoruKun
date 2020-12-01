using UnityEngine;
using System.Collections;


public class SoundPlay : MonoBehaviour
{
    public GameObject pitch;
    GameObject[] capsule = new GameObject[4096];

    private float threshold = 0.04f; //ピッチとして検出する最小の分布

    void Start()
    {
        //音程表示用の箱を作る
        for (int i = 0; i < capsule.Length; i++)
        {
            capsule[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            capsule[i].transform.position = new Vector3(0, (-capsule.Length / 2 + i) * 10.0f / capsule.Length, 0);
            capsule[i].transform.localScale = new Vector3(5, 10.0f / capsule.Length, 1);
        }

        AudioSource aud = GetComponent<AudioSource>();
        // マイク名、ループするかどうか、AudioClipの秒数、サンプリングレート を指定する
        aud.clip = Microphone.Start(null, true, 999, 4410);

        // 録音の準備が出来るまで待つ
        while (!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();
    }

    /*******音声データの周波数成分を解析**********/
    void Update()
    {
        float[] spectrum = new float[8192];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        //graff.GetComponent<CustomInspector>().OnGUI(spectrum);

        /* 低周波な領域(30Hz以下)を除去する(C1から検出する) 
        int lowIndex = 0;
        lowIndex = 30 / AudioSettings.outputSampleRate * 2 * spectrum.Length;
        Debug.Log(lowIndex);
        */

        //Debug.Log(spectrum.Length);
        /********周波数成分の中で最も大きいところ***********/
        var maxIndex = 0;
        var maxIndex2 = 0;
        var maxValue = 0.0f;

        for (int i = 0; i < spectrum.Length; i++)
        {
            var val = spectrum[i];
            //Debug.Log(val);
            /* それぞれの箱の色を変える */
            if (i < capsule.Length)
            {
                capsule[i].GetComponent<Renderer>().material.color = new Color(val * 100, 0, 0, 1);
            }
            if (val > maxValue && val > threshold)
            {
                maxValue = val;
                maxIndex = i;
                maxIndex2 = maxIndex;
            }
        }
        Debug.Log(maxValue);

        // maxValue が最も大きい周波数成分の値で、
        // maxIndex がそのインデックス。欲しいのはこっち。

        float freqN = maxIndex;
        if (maxIndex > 0 && maxIndex < spectrum.Length - 1)
        {
            //隣のスペクトルも考慮する
            float dL = spectrum[maxIndex - 1] / spectrum[maxIndex];
            float dR = spectrum[maxIndex + 1] / spectrum[maxIndex];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        var freq = freqN * (AudioSettings.outputSampleRate / 2) / spectrum.Length;
        
        //サンプリングレート
        //Debug.Log(AudioSettings.outputSampleRate);

        if (freq > 0)
        {
            Debug.Log(freq);
            Debug.Log(pitch.GetComponent<NoteNameDetector>().GetNoteName(freq));
        }
        else
        {
            Debug.Log("No input.");
        }

    }
}