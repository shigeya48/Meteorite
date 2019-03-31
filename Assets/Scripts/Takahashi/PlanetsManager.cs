using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    public int objMaxCount = 10;
    public GameObject titlePlanet;
    public Vector3 pos;         //スポーン位置
    public GameObject target;
    void Update()
    {
        int ObjCount = this.transform.childCount;
        if (objMaxCount > ObjCount)
        {
            GameObject obj = Instantiate(titlePlanet, pos, Quaternion.identity);
            obj.transform.GetComponent<PlanetRotate>().target = target;
            obj.transform.parent = transform;
        }
    }
}
