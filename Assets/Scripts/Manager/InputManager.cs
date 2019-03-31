using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    // 左スティックのイベント
    public event Action<float, float> OnStickInput;

    // Bボタンのイベント
    public event Action OnBButtonInput;

    // メニューボタンのイベント
    public event Action OnMenuButtonInput;

    // ビューボタンのイベント
    public event Action OnViewButtonInput;

    // Xボタンのイベント
    public event Action OnXButtonInput;

    new void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        //取得したインスタンスをシーン上に作成する
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StickInput();

        if (Input.GetButtonDown("Menu_Button"))
        {
            MenuButtonInput();
        }

        if (Input.GetButtonDown("View_Button"))
        {
            ViewButtonInput();
        }

        if (Input.GetButtonDown("B_Button"))
        {
            BButtonInput();
        }

        if (Input.GetButtonDown("X_Button"))
        {
            XButtonInput();
        }
    }

    /// <summary>
    /// 左スティック
    /// </summary>
    void StickInput()
    {
        // AxisInput
        float dx = Input.GetAxisRaw("L_Stick_Horizontal");
        float dy = Input.GetAxisRaw("L_Stick_Vertical");

        if (OnStickInput != null)
        {
            OnStickInput(dx, dy);
        }
    }

    /// <summary>
    /// Bボタン
    /// </summary>
    void BButtonInput()
    {
        if (OnBButtonInput != null)
        {
            OnBButtonInput();
        }
    }

    /// <summary>
    /// Xボタン
    /// </summary>
    void XButtonInput()
    {
        if (OnXButtonInput != null)
        {
            OnXButtonInput();
        }
    }

    /// <summary>
    /// メニューボタン
    /// </summary>
    void MenuButtonInput()
    {
        if (OnMenuButtonInput != null)
        {
            OnMenuButtonInput();
        }
    }

    /// <summary>
    /// ビューボタン
    /// </summary>
    void ViewButtonInput()
    {
        if (OnViewButtonInput != null)
        {
            OnViewButtonInput();
        }
    }
}