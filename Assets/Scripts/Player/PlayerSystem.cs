using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSystem : MonoBehaviour
{
    // rigidbody2d
    Rigidbody2D rd2d;

    // trailrenderer
    TrailRenderer trail;

    // 連続ブースト回数
    bool isCanBoost = false;

    // 回転速度
    float rotSpeed = 40;

    // 移動速度
    float moveSpeed = 5;

    // ブーストの速度(移動距離)
    float boostSpeed = 65;

    // ブースト状態か判断
    bool isBoost = false;

    // ブースト開始時のイベント
    public event Action OnBoost;

    // ブーストヒット時のイベント
    public event Action<Vector2, GameObject> OnBoostHit;

    // ブースト終了時のイベント
    public event Action OnBoostStop;

    // 矢印表示を判断
    bool isDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnStickInput += StickInput;

        InputManager.Instance.OnBButtonInput += BoostInput;

        InputManager.Instance.OnXButtonInput += TrueDirection;

        rd2d = GetComponent<Rigidbody2D>();

        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    void FixedUpdate()
    {
        // 一定速度以内の場合ブーストを止める
        BoostStop();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void StickInput(float dx, float dy)
    {
        if (GameSystem.isGameStop)
        {
            return;
        }

        // スティックが入っている場合 && ブースト状態ではない場合
        if ((dx != 0 || dy != 0) && !isBoost)
        {
            // 回転方向
            float direction = Mathf.Atan2(-dx, dy) * Mathf.Rad2Deg;

            // 回転
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, direction), rotSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, direction);

            // 移動
            rd2d.MovePosition(rd2d.position + new Vector2(dx, dy).normalized * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ブースト
    /// </summary>
    void BoostInput()
    {
        if (isBoost || !isCanBoost || GameSystem.isGameStop)
        {
            return;
        }

        // ブースト状態
        isBoost = true;

        trail.enabled = true;

        // ブースト時イベント
        if (OnBoost != null)
        {
            OnBoost();
        }

        // ブースト
        rd2d.velocity = transform.up.normalized * boostSpeed;
    }

    /// <summary>
    /// 矢印表示
    /// </summary>
    void TrueDirection()
    {
        if (GameSystem.isGameStop)
        {
            return;
        }

        if (!isDirection)
        {
            isDirection = true;
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsDirection", true);
        }
        else
        {
            isDirection = false;
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsDirection", false);
        }
    }

    // 当たり判定(瞬時)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ブースト状態の場合
        if (isBoost && collision.gameObject.GetComponent<PlanetSystem>())
        {
            BoostHit(collision.gameObject);
        }
    }

    // 当たり判定(継続)
    void OnCollisionStay2D(Collision2D collision)
    {
        // ブースト状態の場合
        if (isBoost && collision.gameObject.GetComponent<PlanetSystem>())
        {
            BoostHit(collision.gameObject);
        }
    }

    /// <summary>
    /// ブースト状態でぶつかった場合
    /// </summary>
    /// <param name="planet"></param>
    void BoostHit(GameObject planet)
    {
        // ブーストヒット時イベント
        if (OnBoostHit != null)
        {
            Vector2 direction = (Vector2)planet.transform.position - (Vector2)transform.position;
            direction.Normalize();

            OnBoostHit(direction, planet);
        }

        isBoost = false;
        trail.enabled = false;

        rd2d.velocity = transform.up * 10;
    }

    /// <summary>
    /// ブーストを止める
    /// </summary>
    void BoostStop()
    {
        // 一定以下のスピードの場合ブースト終了
        if (isBoost)
        {
            if (rd2d.velocity.magnitude < 8.0f)
            {
                // ブースト終了時イベント
                if (OnBoostStop != null)
                {
                    OnBoostStop();
                }

                isBoost = false;
                trail.enabled = false;
            }
        }
    }

    public bool IsCanBoost
    {
        set { isCanBoost = value; }
    }

    public void RemoveInputEvent()
    {
        InputManager.Instance.OnStickInput -= StickInput;

        InputManager.Instance.OnBButtonInput -= BoostInput;

        InputManager.Instance.OnXButtonInput -= TrueDirection;
    }
}
