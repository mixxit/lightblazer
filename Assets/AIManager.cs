using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

    ArrayList aiList = new ArrayList();

	// Use this for initialization
	void Start () {
        AI ted = new AI();
        ted.x = 0;
        ted.y = 0;
        aiList.Add(ted);
	}

	// Update is called once per frame
	void Update () {
        foreach(AI ai in aiList)
        {
            ai.Tick();
        }
	
	}
}
