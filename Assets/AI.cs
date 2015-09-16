using UnityEngine;
using System.Collections;
using System;

public class AI : MonoBehaviour {
    Boolean initialised = false;

    internal int x;
    internal int y;
    GameObject sceneobject;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Tick()
    {
        if(initialised = false)
        {
            // Initialise
            Initialise();
        }
    }

    public void Initialise()
    {
        sceneobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sceneobject.transform.position = new Vector3(0, 0, 0); ;
        sceneobject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Color colour = Color.red;
        colour.a = 0.35f;
        sceneobject.GetComponent<Renderer>().material.color = colour;
        initialised = true;
    }
}
