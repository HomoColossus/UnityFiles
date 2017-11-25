using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    GameObject food;
    Transform spawnPosition;
    float spawnSphereRadius;

    void Awake ()
    {
        InitializeFood();

    }

    void Start () 
    {
        // Cannot call InitializeFood due to wrong result. 
        // It seems to be a parallel / asynchronous process of running Start () as the print of position just after initialization is
        // not updated according to the commands in initializefood. Thus, do it in Awake and it works. 
    }
        


    void InitializeFood ()
    {
        transform.SetParent (GameObject.FindGameObjectWithTag("Plane").transform);
        transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 0.3f, Random.Range(-3.0f, 3.0f));
    }



    public Vector3 GetFoodPosition ()
    {
        return transform.position;

    }


    public GameObject GetFoodGO ()
    {
        //print("returning GameObject food");
        return this.gameObject;
    }





}
