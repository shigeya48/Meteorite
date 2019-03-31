using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    PlayerSystem player;

    UIManager playerCanvas;

    ResultInput result;

    InputManager inputManager;

    GameObject button;

    int buttonNum = 0;

    bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerSystem>();

        playerCanvas = FindObjectOfType<UIManager>();

        result = FindObjectOfType<ResultInput>();

        inputManager = FindObjectOfType<InputManager>();

        inputManager.OnMenuButtonInput += PauseInput;

        inputManager.OnViewButtonInput += QuickRetry;

        inputManager.OnBButtonInput += OkButton;

        inputManager.OnStickInput += SelectMenu;

        button = transform.Find("PauseText").Find("Retry").Find("Button").gameObject;

        GetComponent<Canvas>().enabled = false;
    }

    void PauseInput()
    {
        if (!GameSystem.isGameStop)
        {
            Time.timeScale = 0;

            AudioManager.Instance.PlaySE("Pause_OkButton");

            GameSystem.isGameStop = true;

            isPause = true;

            GetComponent<Canvas>().enabled = true;
            playerCanvas.gameObject.GetComponent<Canvas>().enabled = false;

            buttonNum = 0;
            button.transform.SetParent(transform.Find("PauseText").Find("Retry"));
            button.GetComponent<RectTransform>().localPosition = new Vector3(130, -50, 0);
        }
        else if (GameSystem.isGameStop && isPause)
        {
            Back();
        }
    }

    void QuickRetry()
    {
        if (!GameSystem.isGameStop)
        {
            Retry();
        }
    }

    void Retry()
    {
        Time.timeScale = 1;

        AudioManager.Instance.PlaySE("Pause_OkButton");

        player.RemoveInputEvent();
        RemoveInputEvent();

        SceneFade.Instance.LoadScene("Game");
    }

    void Title()
    {
        Time.timeScale = 1;

        AudioManager.Instance.PlaySE("Pause_OkButton");

        player.RemoveInputEvent();
        RemoveInputEvent();

        SceneFade.Instance.LoadScene("Title");
    }

    void Back()
    {
        Time.timeScale = 1;

        AudioManager.Instance.PlaySE("Pause_OkButton");

        GameSystem.isGameStop = false;

        isPause = false;

        GetComponent<Canvas>().enabled = false;
        playerCanvas.gameObject.GetComponent<Canvas>().enabled = true;
    }

    void SelectMenu(float dx, float dy)
    {
        if (GameSystem.isGameStop && isPause)
        {
            if (dy > 0)
            {
                if (buttonNum > 0)
                {
                    buttonNum--;

                    AudioManager.Instance.PlaySE("ButtonSelect");

                    button.transform.SetParent(transform.Find("PauseText").GetChild(buttonNum));
                    button.GetComponent<RectTransform>().localPosition = new Vector3(130, -50, 0);
                }
            }
            else if (dy < 0)
            {
                if (buttonNum < 1)
                {
                    buttonNum++;

                    AudioManager.Instance.PlaySE("ButtonSelect");

                    button.transform.SetParent(transform.Find("PauseText").GetChild(buttonNum));
                    button.GetComponent<RectTransform>().localPosition = new Vector3(130, -50, 0);
                }
            }
        }
    }

    void OkButton()
    {
        if (GameSystem.isGameStop && isPause)
        {
            switch(buttonNum)
            {
                case 0:
                    Retry();
                    break;
                case 1:
                    Title();
                    break;
            }
        }
    }

    public void RemoveInputEvent()
    {
        inputManager.OnMenuButtonInput -= PauseInput;

        inputManager.OnViewButtonInput -= QuickRetry;

        inputManager.OnBButtonInput -= OkButton;

        inputManager.OnStickInput -= SelectMenu;
    }
}
