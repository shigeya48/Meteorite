using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    // インプット等キャンセルするフラグ
    public static bool isGameStop = false;

    // 現在のスコア
    int score = 0;

    // 現在のコンボ数
    int combo = 0;

    // 現在の最大コンボ数
    int maxCombo = 0;

    // timerのインターバル
    const float timeInterval = 60;

    // タイマー
    float timer = 0;

    // プレイヤー
    PlayerSystem player;

    // プラネットマネージャー
    PlanetManager planetManager;

    // コンボシステム
    ComboSystem comboSystem;

    // インプットマネージャー
    InputManager inputManager;

    // スコア加算時のイベント
    public event Action<int> OnScoreAdd;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>();

        planetManager = FindObjectOfType<PlanetManager>();

        comboSystem = FindObjectOfType<ComboSystem>();

        comboSystem.OnComboAdd += ScoreAdd;

        timer = timeInterval;

        score = 0;

        AudioManager.Instance.PlayBGM("gameBGM01");
        AudioManager.Instance.ChangeVolume(0.5f, 1.0f);

        // ゲームスタート
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStop)
        {
            return;
        }

        Timer();
    }

    /// <summary>
    /// 制限時間
    /// </summary>
    void Timer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            // ゲームオーバー
            StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStart()
    {
        isGameStop = true;

        FindObjectOfType<UIManager>().GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        transform.Find("GameStartCanvas").GetComponent<Animator>().SetTrigger("CountStart");
        transform.Find("GameStartCanvas").GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        yield return new WaitForSeconds(3.0f);

        isGameStop = false;
        FindObjectOfType<UIManager>().GetComponent<Canvas>().enabled = true;

        yield return null;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="comboCount">現在のコンボ数</param>
    /// <param name="maxComboCount">最大コンボ数</param>
    /// <param name="scoreOffset">スコアの元の値</param>
    void ScoreAdd(int comboCount, int maxComboCount, int scoreOffset)
    {
        combo = comboCount;

        maxCombo = maxComboCount;

        // スコア計算
        float scoreTemp = scoreOffset * (1 + (float)comboCount / 10);

        score += (int)scoreTemp;

        if (OnScoreAdd != null)
        {
            OnScoreAdd(score);
        }
    }

    /// <summary>
    /// 残り時間を返す
    /// </summary>
    public float TimerValue
    {
        get { return timer; }
    }

    /// <summary>
    /// ゲームクリアメソッド
    /// </summary>
    public void GameClear()
    {
        if (!isGameStop)
        {
            StartCoroutine(DoGameClear());
        }
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    /// <returns></returns>
    IEnumerator DoGameClear()
    {
        isGameStop = true;

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(0.1f);

        // 演出
        FindObjectOfType<UIManager>().GetComponent<Canvas>().enabled = false;
        transform.Find("GameClearCanvas").gameObject.SetActive(true);
        transform.Find("GameClearCanvas").GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        if (planetManager.FullComboCount == maxCombo)
        {
            //フルコンボ
            transform.Find("GameClearCanvas").GetComponent<Animator>().SetTrigger("FullCombo");
        }

        yield return new WaitForSeconds(0.3f);

        // リザルト表示
        StartCoroutine(Resulut("Game Clear"));

        yield return null;
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        isGameStop = true;

        player.GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(0).GetComponent<Animator>().SetBool("IsDirection", false);

        Instantiate(EffectData.Instance.planetExplosionEffect, new Vector3(player.transform.position.x, player.transform.position.y, 20), Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0.1f;

        FindObjectOfType<UIManager>().GetComponent<Canvas>().enabled = false;
        transform.Find("GameOverCanvas").gameObject.SetActive(true);
        transform.Find("GameOverCanvas").GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(Resulut("Game Over"));

        yield return null;
    }

    /// <summary>
    /// リザルト表示
    /// </summary>
    /// <param name="resulut"></param>
    /// <returns></returns>
    IEnumerator Resulut(string resulut)
    {
        GameObject resulutCanvas = transform.Find("ResultCanvas").gameObject;

        // スコア、最大コンボ、残り時間、合計スコア
        resulutCanvas.SetActive(true);
        resulutCanvas.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        int totalScore = score + (int)timer * 150;

        resulutCanvas.transform.Find("Result").GetComponent<Text>().text = resulut;
        resulutCanvas.transform.Find("Score").GetChild(0).GetComponent<Text>().text = score.ToString();
        resulutCanvas.transform.Find("Combo").GetChild(0).GetComponent<Text>().text = maxCombo.ToString();
        resulutCanvas.transform.Find("Time").GetChild(0).GetComponent<Text>().text = timer.ToString("f0");
        resulutCanvas.transform.Find("TotalScore").GetChild(0).GetComponent<Text>().text = totalScore.ToString();

        player.RemoveInputEvent();

        yield return null;
    }
}
