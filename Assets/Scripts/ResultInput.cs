using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultInput : MonoBehaviour
{
    PauseSystem pause;

    GameObject button;

    bool isInput = false;

    // Start is called before the first frame update
    void Start()
    {
        pause = FindObjectOfType<PauseSystem>();

        InputManager.Instance.OnStickInput += SelectButton;

        InputManager.Instance.OnBButtonInput += OkButton;

        button = transform.Find("Retry").Find("BButton").gameObject;
    }

    void SelectButton(float dx, float _)
    {
        if (isInput)
        {
            if (dx > 0 && button.transform.parent != transform.Find("Retry"))
            {
                AudioManager.Instance.PlaySE("ButtonSelect");
                button.transform.SetParent(transform.Find("Retry"));
                button.GetComponent<RectTransform>().localPosition = new Vector3(80, -40, 0);
            }
            else if (dx < 0 && button.transform.parent != transform.Find("Title"))
            {
                AudioManager.Instance.PlaySE("ButtonSelect");
                button.transform.SetParent(transform.Find("Title"));
                button.GetComponent<RectTransform>().localPosition = new Vector3(80, -40, 0);
            }
        }
    }

    void OkButton()
    {
        if (isInput)
        {
            if (button.transform.parent == transform.Find("Title"))
            {
                SceneFade.Instance.LoadScene("Title");
            }
            else
            {
                SceneFade.Instance.LoadScene("Game");
            }

            AudioManager.Instance.PlaySE("Pause_OkButton");
            RemoveInputEvent();
            pause.RemoveInputEvent();

            Time.timeScale = 1.0f;
            isInput = false;
        }
    }

    void IsInput()
    {
        isInput = true;
    }

    public void RemoveInputEvent()
    {
        InputManager.Instance.OnStickInput -= SelectButton;
        InputManager.Instance.OnBButtonInput -= OkButton;
    }
}
