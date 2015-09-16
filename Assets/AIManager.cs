using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

    private Transform genericmob;
    ArrayList aiList = new ArrayList();

	// Use this for initialization
	void Start () {
        genericmob = Resources.Load<Transform>("Mobs/TemplateMob");
        Transform mob = Instantiate(genericmob, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
