using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAudio : MonoBehaviour
{
    void StartCountAudio()
    {
        AudioManager.Instance.PlaySE("StartCount");
    }

    void GameClear_Over_TextAudio()
    {
        AudioManager.Instance.PlaySE("GameClear_Over_Text");
    }

    void ResultAudio()
    {
        AudioManager.Instance.PlaySE("ButtonSelect");
    }
}
