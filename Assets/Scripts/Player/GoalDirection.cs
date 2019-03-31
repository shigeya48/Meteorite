using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDirection : MonoBehaviour
{
    // PlanetManager
    PlanetManager planetManager;

    // 表示非表示を切り替えるフラグ
    bool isActiv = false;

    // Animator
    Animator anim;

    // カメラ範囲
    Rect rect = new Rect(0, 0, 1, 1);

    // ゲームクリア条件のプラネット
    ClearPlanet clearPlanet;

    // プレイヤー
    PlayerSystem player;

    // Start is called before the first frame update
    void Start()
    {
        planetManager = FindObjectOfType<PlanetManager>();

        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;

        clearPlanet = FindObjectOfType<ClearPlanet>();

        player = FindObjectOfType<PlayerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームストップ時と、クリアプラネットがない場合はUpdateを行わない
        if (GameSystem.isGameStop || clearPlanet == null)
        {
            // 矢印は非表示に
            anim.SetBool("isFade", false);
            return;
        }

        // カメラ範囲内をチェックする
        CheckInCamera();

        // 表示
        if (!isActiv)
        {
            anim.SetBool("isFade", true);
        }
        // 非表示
        else
        {
            anim.SetBool("isFade", false);
        }

        // 矢印の方向を指定
        DirectionPos();
    }

    /// <summary>
    /// カメラ内をチェックする
    /// </summary>
    void CheckInCamera()
    {
        for (int i = 0; i < planetManager.planets.Count; i++)
        {
            // プラネットの座標を取得
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(planetManager.planets[i].transform.position);

            // カメラ範囲内
            if (rect.Contains(viewportPos))
            {
                isActiv = true;
                break;
            }
            // カメラ範囲外
            else
            {
                isActiv = false;
            }
        }
    }

    /// <summary>
    /// 矢印の方向と位置を設定する
    /// </summary>
    void DirectionPos()
    {
        Vector2 direction = (clearPlanet.transform.position - player.transform.position).normalized;

        float rotDirection = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

        transform.position = (Vector2)Camera.main.transform.position + direction * new Vector2(10, 6);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotDirection), 10 * Time.deltaTime);
    }
}
