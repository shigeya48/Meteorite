using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetSystem : MonoBehaviour
{
    // プラネットマネージャー
    PlanetManager manager;

    // プラネットごとのスコアを設定
    [SerializeField, Header("加算する得点")]
    int planetScore = 100;

    // プラネット破壊時のイベント
    public event Action OnDestroyPlanet;

    void Start()
    {
        manager = FindObjectOfType<PlanetManager>();
    }

    /// <summary>
    /// プラネット破壊
    /// </summary>
    public void DestroyPlanet()
    {
        manager.RemovePlanet(gameObject);

        if (OnDestroyPlanet != null)
        {
            OnDestroyPlanet();
        }

        // 破壊時エフェクト
        Instantiate(EffectData.Instance.planetExplosionEffect, new Vector3(transform.position.x, transform.position.y, 30), Quaternion.identity);

        // 破壊時サウンド
        AudioManager.Instance.PlaySE("BoostHit02");
        Destroy(this.gameObject);
    }

    /// <summary>
    /// プラネット自体のスコア値を渡す
    /// </summary>
    public int PlanetScore
    {
        get { return planetScore; }
    }
}
