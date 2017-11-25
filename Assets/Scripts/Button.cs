using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {


    public Color defaultColor;
    public Color selectedColor;
    private Material mat;

    private GameObject WorldScript;
    private GameObject food;

    void Start ()
    {
        mat = GetComponent<Renderer>().material;
        WorldScript = GameObject.FindGameObjectWithTag("WorldScript");
    }



    void OnTouchDown ()
    {
        mat.color = selectedColor;

        food = Instantiate (Resources.Load("Prefabs/food", typeof(GameObject))) as GameObject;

        WorldScript.GetComponent<WorldScript>().RecieveFoodGO(food);

    }


    void OnTouchUp ()
    {
        mat.color = defaultColor;

    }


    void OnTouchStay ()
    {
        mat.color = selectedColor;

    }


    void OnTouchExit ()
    {
        mat.color = defaultColor;

    }



}
