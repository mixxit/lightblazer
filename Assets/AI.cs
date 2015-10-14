using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AI : MonoBehaviour {
    public Cell cell;
    public Board board;
    Cell pendingmove;
    Cell heading;
    GameObject job;

    List<Transform> pathinglines = new List<Transform>();

    // Use this for initialization
    void Start () {
        cell = board.GetRandomWalkableCell();

    }

    // Update is called once per frame
    void Update () {

        
	}


    public void Think()
    {
        Move();
        transform.position = cell.transform.position;
        HandleJob();
    }

    public void HandleJob()
    {
        if (job != null)
        {
            if (job.gameObject.tag.Equals("Terrain"))
            {
                heading = job.GetComponent<Cell>();
                //Debug.LogWarning("Handling Job: " + job.tag);
            }
            if (job.gameObject.tag.Equals("Harvestable"))
            {
                Debug.LogWarning("I have a new job");

                AStar pathFinder = new AStar();
                pathFinder.FindPath(cell, job.gameObject.GetComponent<Cell>(), cell.GetComponentInParent<Board>().board, false);
                List<Cell> path = pathFinder.CellsFromPath();
                Debug.Log("Found path to harvestable : " + path.Count);

                switch(path.Count)
                {
                    case 0:
                        heading = cell;
                        job = null;
                        Debug.LogWarning("Job aborted impossible to reach");
                        break;
                    case 1:
                        board.RemoveCell(job.gameObject.GetComponent<Cell>());

                        heading = cell;
                        job = null;
                        Debug.LogWarning("Job complete");
                        break;
                    default:
                        heading = path[path.Count - 1];
                        break;
                }
            }
        }


    }

    public Boolean isSelected()
    {
        return transform.Find("Selector").gameObject.activeSelf;
    }

    public GameObject GetJob()
    {
        return job;
    }

    public void SetJob(GameObject obj)
    {
        this.job = obj;
    }

    void HandleInteract(GameObject target)
    {
        if (isSelected())
        {
            Debug.Log("Handling interact for " + target.tag);
            if (target.tag.Equals("Terrain"))
            {
                MoveTo(target.GetComponent<Cell>());
            }
        }
    }

    void HandleSelection(SelectionArea area)
    {
        if (gameObject.transform.position.x >= area.startvector.x && gameObject.transform.position.x <= area.endvector.x && gameObject.transform.position.y >= area.startvector.y && gameObject.transform.position.y <= area.endvector.y)
        {
            Debug.LogWarning("Handling selection on mob");
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

    public void MoveTo(Cell cell)
    {
        Debug.LogWarning("Moving to Cell: " + cell.coordinates.x + "," + cell.coordinates.y);
        heading = cell;
    }

    public void Tick()
    {
        Move();
    }
    
    public void Move()
    {
        foreach(Transform pathingline in pathinglines)
        {
            Destroy(pathingline.gameObject);
        }

        pathinglines.Clear();

        if (heading != null)
        {
            if (cell != heading)
            {
                AStar pathFinder = new AStar();
                pathFinder.FindPath(cell, heading, cell.GetComponentInParent<Board>().board, false);
                List<Cell> path = pathFinder.CellsFromPath();

                if (path.Count > 0)
                {
                    pendingmove = path[0];
                } else
                {
                    Debug.LogWarning("Target location is unreachable");


                    // IF this is our job clear it
                    if (job != null)
                    {
                        if (job.GetComponent<Cell>().Equals(heading))
                        {
                            job = null;
                        }
                    }

                    heading = cell;
                    
                }
            }
        }

        if (pendingmove != null)
        {
            if (cell != pendingmove)
            {
                Debug.LogWarning("Pending move didn't match position");
                cell = pendingmove;

                if (job != null)
                {
                    if (job.GetComponent<Cell>().Equals(pendingmove))
                    {
                        job = null;
                    }
                }
            }
        }
    }
}
