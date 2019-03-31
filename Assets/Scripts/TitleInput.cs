using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnBButtonInput += SceneChange;

        AudioManager.Instance.PlayBGM("TitleBGM01");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneChange()
    {
        if (!SceneFade.Instance.NowFade)
        {
            AudioManager.Instance.PlaySE("Pause_OkButton");
            InputManager.Instance.OnBButtonInput -= SceneChange;
            SceneFade.Instance.LoadScene("Game");
        }
    }
}
