using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    
    public GameObject planetExplosionEffect;
    public float multiple;
    [HideInInspector]public GameObject target;
    
    Vector3 rotatespeed = new Vector3( 0 , 1, 0 );
    float moveSpeed;
    bool isCalled;


    void Start()
    {
        if (target != null)
        { 
            int i = Random.Range(0, 2);
            if (i == 0) i = -1;
            moveSpeed = Random.Range(50.0f, 500.0f) * i;
            transform.position += new Vector3(Random.Range(-100.00f, 101.00f), Random.Range(-25.00f, 26.00f), Random.Range(-100.00f, 101.00f));
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-15.00f, 16.00f));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            multiple = target.GetComponent<PlanetRotate>().multiple;
            Vector3 axis = transform.TransformDirection(Vector3.up);
            transform.RotateAround(target.transform.position, axis, moveSpeed / multiple * Time.deltaTime);
        }
        transform.Rotate(rotatespeed.x * Random.Range(0.750f, 1.250f) / multiple, rotatespeed.y * Random.Range(0.750f, 5.250f) / multiple, rotatespeed.z * Random.Range(0.750f, 1.250f) / multiple);
    }
    void OnWillRenderObject()
    {
        if (Camera.current.name == "Main Camera")
        {
            if (target != null &&isCalled == false)
            {
                int i = Random.Range(0, 3);
                isCalled = true;
                if (i == 0 ||i == 1)
                {
                    StartCoroutine(DestroyPlanet(Random.Range(10.25f,20.00f)));
                }
            }
        }
        else if(Camera.current == null)isCalled = false;
    }
    private IEnumerator DestroyPlanet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(planetExplosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
