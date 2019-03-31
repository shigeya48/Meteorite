using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ComboSystem : MonoBehaviour
{
    // プレイヤー
    PlayerSystem player;

    // コンボエフェクト
    GameObject comboEffect = null;

    // 現在のコンボ数
    int comboCount = 0;

    // 最大コンボ数
    int maxComboCount = 0;

    // タイマー
    float timer = 0;

    // コンボ維持時間
    const float comboInterval = 1.0f;

    // コンボ継続中か判断するフラグ
    bool isCombo = false;

    // コンボ加算時のイベント
    public event Action<int, int, int> OnComboAdd;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>();

        player.OnBoostHit += ComboAdd;

        player.OnBoostStop += ComboStop;
    }

    void Update()
    {
        if (isCombo)
        {
            timer += Time.deltaTime;

            // 一定時間経過したらコンボが途切れる
            if (timer > comboInterval)
            {
                ComboStop();
            }
        }
    }

    /// <summary>
    /// コンボ加算
    /// </summary>
    /// <param name="direction">プレイヤーの方向</param>
    /// <param name="planet">プラネットオブジェクト</param>
    void ComboAdd(Vector2 direction, GameObject planet)
    {
        // コンボ開始
        isCombo = true;
        timer = 0;
        comboCount++;

        // 2コンボ以上の場合
        if(comboCount > 1)
        {
            // コンボ演出をする
            if (comboEffect != null)
            {
                Destroy(comboEffect);
            }
            comboEffect = Instantiate(EffectData.Instance.comboEffect, planet.transform.position - new Vector3(direction.x * 3, direction.y * 3, 3.0f), Quaternion.identity);
            comboEffect.transform.Find("ComboNum").GetComponent<Text>().text = comboCount.ToString();
        }

        // 現在のコンボが最大コンボ数より多い場合
        if (maxComboCount < comboCount)
        {
            maxComboCount = comboCount;
        }

        // コンボ加算時のイベント
        if (OnComboAdd != null)
        {
            OnComboAdd(comboCount, maxComboCount, planet.GetComponent<PlanetSystem>().PlanetScore);
        }

        // プラネットオブジェクトを破壊する
        planet.GetComponent<PlanetSystem>().DestroyPlanet();
    }

    /// <summary>
    /// コンボストップ
    /// </summary>
    void ComboStop()
    {
        isCombo = false;
        comboCount = 0;
    }
}
