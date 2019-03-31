using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // スコアのテキスト
    Text scoreText;

    // タイマーのテキスト
    Text timerText;

    // プレイヤー
    PlayerSystem player;

    // GameSystem
    GameSystem gameSystem;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>();

        gameSystem = FindObjectOfType<GameSystem>();

        timerText = transform.Find("Time").GetComponent<Text>();

        scoreText = transform.Find("Score").GetComponent<Text>();

        gameSystem.OnScoreAdd += ScoreText;

        scoreText.text = "Score : " + "0";
    }

    void Update()
    {
        timerText.text = gameSystem.TimerValue.ToString("f0");
    }

    /// <summary>
    /// スコアの更新
    /// </summary>
    /// <param name="score">スコアの値</param>
    void ScoreText(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }
}
