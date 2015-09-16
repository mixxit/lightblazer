using UnityEngine;
using System.Collections;
using System;

public class AI : MonoBehaviour {
    internal int x;
    internal int y;

    public Boolean selected;

    // Use this for initialization
    void Start () {
         selected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            transform.Find("Selector").gameObject.SetActive(true);
        } else
        {
            transform.Find("Selector").gameObject.SetActive(false);
        }
	}

    void OnTriggerExit2D(Collider2D otherObj)
    {
        Debug.Log("Exit");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogWarning("Entered collision");
    }

    public void Tick()
    {
        Move();
    }

    public void Move()
    {
        transform.position = new Vector3(x, y, 0);
    }
}
