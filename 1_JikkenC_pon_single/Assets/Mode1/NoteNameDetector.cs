using UnityEngine;
using System.Collections;

/********音名の定義***********/
public class NoteNameDetector : MonoBehaviour
{
    private string[] noteNames = { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };

    public string GetNoteName(float freq)
    {
        // 周波数からMIDIノートナンバーを計算
        var noteNumber = calculateNoteNumberFromFrequency(freq);
        // 0:C - 11:B に収める
        var note = noteNumber % 12;
     //   if (note < 0) note += 12;
        // 0:C～11:Bに該当する音名を返す
 //       Debug.Log(noteNumber);
        return noteNames[note];
    }

    // See https://en.wikipedia.org/wiki/MIDI_tuning_standard
    /* 周波数をノートナンバーに変換する関数 */
    private int calculateNoteNumberFromFrequency(float freq)
    {
        //小数点以下切り捨てver！！
        return Mathf.CeilToInt(69 + 12 * Mathf.Log(freq / 440, 2));

        //四捨五入ver
        //return Mathf.RoundToInt(69 + 12 * Mathf.Log(freq / 440, 2));
    }
}
