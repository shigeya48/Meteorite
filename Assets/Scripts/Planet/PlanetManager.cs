using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    // すべてのプラネットの数
    public List<PlanetSystem> planets = new List<PlanetSystem>();

    // 惑星の数(最大コンボ数)
    int planetLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlanetSystem[] temp = FindObjectsOfType<PlanetSystem>();

        planetLength = temp.Length;

        // プラネットをすべて取得
        foreach(PlanetSystem planet in temp)
        {
            planets.Add(planet);
        }
    }

    /// <summary>
    /// プラネットの登録を解除
    /// </summary>
    /// <param name="planet"></param>
    public void RemovePlanet(GameObject planet)
    {
        foreach(PlanetSystem RemoveItem in planets.ToArray())
        {
            if (RemoveItem.gameObject == planet)
            {
                planets.Remove(RemoveItem);
            }
        }
    }

    /// <summary>
    /// すべてのプラネットの数(フルコンボ数)
    /// </summary>
    public int FullComboCount
    {
        get { return planetLength; }
    }
}