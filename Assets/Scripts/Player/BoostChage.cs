using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostChage : MonoBehaviour
{
    // プレイヤー
    PlayerSystem player;

    // スライダー
    Slider boostGauge;

    // ゲージが溜まるインターバル
    const float chageTimeInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>();

        boostGauge = GetComponent<Slider>();

        player.OnBoost += BoostDecrease;

        player.OnBoostHit += BoostAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.isGameStop)
        {
            return;
        }

        if (boostGauge.value < 400)
        {
            boostGauge.value += 24 * Time.deltaTime;
        }

        if (boostGauge.value >= 100)
        {
            player.IsCanBoost = true;
        }
        else
        {
            player.IsCanBoost = false;
        }
    }

    /// <summary>
    /// ブースト消費
    /// </summary>
    void BoostDecrease()
    {
        boostGauge.value -= 100;
        if (boostGauge.value < 0)
        {
            boostGauge.value = 0;
        }
    }

    /// <summary>
    /// ブースト加算(回復)
    /// </summary>
    /// <param name="_"></param>
    /// <param name="__"></param>
    void BoostAdd(Vector2 _, GameObject __)
    {
        boostGauge.value += 100;
        if (boostGauge.value > 400)
        {
            boostGauge.value = 400;
        }
    }
}
