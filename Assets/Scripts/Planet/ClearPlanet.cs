using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlanet : MonoBehaviour
{
    // GameSystem
    GameSystem gameSystem;

    // プラネット
    PlanetSystem planet;

    void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();

        planet = GetComponent<PlanetSystem>();
        
        planet.OnDestroyPlanet += Clear;
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    public void Clear()
    {
        gameSystem.GameClear();
    }
}
