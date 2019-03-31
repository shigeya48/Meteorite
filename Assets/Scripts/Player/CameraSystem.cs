using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // プレイヤー
    GameObject player;

    // プレイヤーの向いている方向にカメラを寄せる距離
    float cameraOffset = 3.0f;

    // カメラの移動スピード
    float moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>().gameObject;

        player.GetComponent<PlayerSystem>().OnBoostHit += WindowShake;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x + (player.transform.up.x * cameraOffset), moveSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, player.transform.position.y + (player.transform.up.y * cameraOffset), moveSpeed * Time.deltaTime), transform.position.z);
    }

    /// <summary>
    /// カメラを揺らすメソッド
    /// </summary>
    /// <param name="direction">揺らす方向</param>
    /// <param name="_"></param>
    void WindowShake(Vector2 direction, GameObject _)
    {
        StartCoroutine(DoWindowShake(direction));
    }

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    /// <param name="direction">揺らす方向</param>
    /// <returns></returns>
    IEnumerator DoWindowShake(Vector2 direction)
    {
        Vector3 offsetPos = transform.localPosition;

        transform.localPosition = transform.localPosition + (Vector3)direction * 0.5f;

        yield return new WaitForSeconds(0.01f);

        transform.localPosition = offsetPos;
    }
}
