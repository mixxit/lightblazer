using UnityEngine;
using System.Collections;
using System;

public class AI : MonoBehaviour {
    internal float x;
    internal float y;

    float movingtox;
    float movingtoy;

    // Use this for initialization
    void Start () {
    }
    	
	// Update is called once per frame
	void Update () {

        Move();
	}

    Boolean isSelected()
    {
        return transform.Find("Selector").gameObject.activeSelf;
    }

    void HandleInteract(GameObject target)
    {
        if (isSelected())
        {
            Debug.Log("Handling interact for " + target.tag);
            if (target.tag.Equals("Terrain"))
            {
                MoveTo(target.transform.position.x, target.transform.position.y);
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

    public void MoveTo(float targetx, float targety)
    {
        Debug.LogWarning("Moving to location " + targetx + "," + targety);
        movingtox = targetx;
        movingtoy = targety;
    }

    public void Tick()
    {
        Move();
    }

    public Vector2 FindAdjacentTileClosestToDestination(Vector2 location, Vector2 destination)
    {
        
        float minx = location.x - 1;
        float miny = location.y - 1;
        float maxx = location.x + 1;
        float maxy = location.y + 1;

        Vector2 choice = location;
        for (float x = minx; x <= maxx; x++)
        {
            for(float y = miny; y <= maxy; y++)
            {
                Vector2 cur = new Vector2(x, y);
                float distance = Vector3.Distance(cur, destination);
                if (distance < Vector3.Distance(choice, destination))
                {

                    Vector2 rayCastLocation = new Vector2(x,y);
                    RaycastHit2D hit = Physics2D.Raycast(rayCastLocation, -Vector2.up);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag.Equals("Terrain"))
                        {
                            choice = cur;
                        }
                    }

                    
                }

            }
        }
        return choice;
    }

    public void Move()
    {
        if (x != movingtox || y != movingtoy)
        {
            Debug.LogWarning("Trying to determine best way to get to " + movingtox + "," + movingtoy);

            // Find adjacent spot to move to
            Vector2 moveto = FindAdjacentTileClosestToDestination(new Vector2(x, y), new Vector2(movingtox, movingtoy));
            Debug.Log("Moving to " + moveto.x + "," + moveto.y);
            x = moveto.x;
            y = moveto.y;
        }

        if (x != transform.position.x || y != transform.position.y)
        {
            transform.position = new Vector3(x, y, 0);
        }
    }
}
