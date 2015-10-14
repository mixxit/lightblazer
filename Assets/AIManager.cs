using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

    private Transform genericmob;
    private Transform genericmob2;
    ArrayList aiList = new ArrayList();
    ArrayList interactList = new ArrayList();
    float timeLeft = 0.5f;

    // Use this for initialization
    void Start () {
        Board board = GetComponent<Board>();
        
        genericmob = Resources.Load<Transform>("Mobs/TemplateMob");
        genericmob.GetComponent<AI>().board = board;
        Transform mob = Instantiate(genericmob, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
        aiList.Add(mob.gameObject);

/*        genericmob2 = Resources.Load<Transform>("Mobs/TemplateMob");
        genericmob2.GetComponent<AI>().board = board;
        Transform mob2 = Instantiate(genericmob2, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
        aiList.Add(mob2.gameObject);*/

        
    }

    // Update is called once per frame
    void Update ()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Think();
            timeLeft = 0.5f;
        }

        HandleInteract();
	}

    public void Think()
    {
        foreach(GameObject aigo in aiList)
        {
            AI ai = aigo.GetComponent<AI>();
            ai.Think();
        }

    }

    public void AddNewJob(GameObject obj)
    {
        int issued = 0;
        foreach(GameObject mob in aiList)
        {
            if (mob.GetComponent<AI>().isSelected())
            {
                mob.GetComponent<AI>().SetJob(obj);
                issued++;
            }
        }

        if (issued == 0)
        {
            interactList.Add(obj);
        }

    }


    void HandleInteract()
    {
        if (interactList.Count > 0)
        {
            foreach (GameObject interactobj in interactList)
            {
                foreach (GameObject mob in aiList)
                {
                    Debug.LogWarning("Assigned job " + interactobj.tag);
                    AI ai = mob.GetComponent<AI>();
                    if (ai.GetJob() == null)
                    {
                        ai.SetJob(interactobj);
                    }
                }
            }
        }

        interactList.Clear();
    }

}
