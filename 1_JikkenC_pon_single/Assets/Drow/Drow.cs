using UnityEngine;
using System.Collections;

public class Drow : MonoBehaviour
{
    // 波形を描画する
    public LineRenderer line;


    // 波形描画のための変数
    private float[] wave;
    private int wave_num;
    private int wave_count;


    void Start()
    {
        // 波形描画のための変数の初期化
        wave_num = 300;
        wave = new float[wave_num];
        wave_count = 0;

    }

    void Update()
    {

        // 波形描画
        wave[wave_count] = 0.1f * wave_count;
        SoundLibrary.ScaleWave(wave, wave_count, line);
        wave_count++;
        if (wave_count >= wave_num) wave_count = 0;
    }
}