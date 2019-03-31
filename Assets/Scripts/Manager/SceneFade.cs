using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : SingletonMonoBehaviour<SceneFade>
{
    // フェードのかかる時間
    public float fadeInterval = 1;

    // 現在のフェード状況(0～1)
    float fadeScale = 0;

    // 現在フェード中かどうか
    bool isFade = false;

    // フェードに使うマテリアル
    Material fadeMaterial;

    new void Awake()
    {
        // すでにインスタンス化されている場合は削除
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        // シーン遷移しても削除されない
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        fadeMaterial = transform.GetChild(0).GetComponent<Image>().material;
    }

    /// <summary>
    /// フェードとシーン遷移を行う
    /// </summary>
    /// <param name="scene">シーン名</param>
    public void LoadScene(string scene)
    {
        // フェード中でない場合
        if (!isFade)
        {
            StartCoroutine(ChangeScene(scene));
        }
    }

    /// <summary>
    /// フェードのみ行う
    /// </summary>
    public void Fade()
    {
        // フェード中でない場合
        if (!isFade)
        {
            StartCoroutine(ChangeScene());
        }
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <returns></returns>
    IEnumerator ChangeScene(string scene = "")
    {
        // フェード開始
        isFade = true;

        // タイマーをリセット
        float timer = 0;

        // タイマー
        while(timer <= fadeInterval)
        {
            // フェードの値
            fadeScale = Mathf.Lerp(0f, 1f, timer / fadeInterval);

            // フェードを適用
            fadeMaterial.SetFloat("_MaskScale", fadeScale);

            // タイマーのカウント
            timer += Time.deltaTime;

            yield return null;
        }

        // フェードの値を1にする
        fadeMaterial.SetFloat("_MaskScale", 1);

        // 引数のシーン名がある場合
        if (scene != "")
        {
            // シーン遷移
            SceneManager.LoadScene(scene);
        }

        // タイマーのリセット
        timer = 0;

        // タイマー
        while (timer <= fadeInterval)
        {
            // フェードの値
            fadeScale = Mathf.Lerp(1f, 0f, timer / fadeInterval);

            // フェードの適用
            fadeMaterial.SetFloat("_MaskScale", fadeScale);

            // タイマーのカウント
            timer += Time.deltaTime;

            yield return null;
        }

        // フェードの値を0にする
        fadeMaterial.SetFloat("_MaskScale", 0);

        // フェード終了
        isFade = false;
    }

    // フェードが行われているかどうかを渡すプロパティ
    public bool NowFade
    {
        get { return isFade; }
    }
}
