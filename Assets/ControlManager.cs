using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    const int orthographicSizeMin = 10;
    const int orthographicSizeMax = 20;

    private Vector2 dragStartPos = Vector2.zero;
    private Vector2 dragEndPosition = Vector2.zero;
    public Texture tex;
    

    // Use this for initialization
    void Start() {
        Camera.main.orthographicSize = 10;
    }

    // Update is called once per frame
    void Update() {
        handleScrollUpdate();
        handleSelectSecondary();
        handleDragSelect();
        handleKeys();
    }

    void OnGUI()
    {
        drawSelectionBox();
    }

    void handleSelectSecondary()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 rayCastLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayCastLocation, -Vector2.up);
            if (hit.collider != null)
            {
                Debug.Log("Hit " + hit.collider.tag);
                gameObject.GetComponent<AIManager>().AddNewJob(hit.collider.gameObject);
            }
        }
    }

    void handleDragSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
        } 

        if (Input.GetMouseButton(0))
        {
            dragEndPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
            SelectionArea area = new SelectionArea();
            area.startvector = Vector2.Min(Camera.main.ScreenToWorldPoint(dragStartPos), Camera.main.ScreenToWorldPoint(dragEndPosition));
            area.endvector = Vector2.Max(Camera.main.ScreenToWorldPoint(dragStartPos), Camera.main.ScreenToWorldPoint(dragEndPosition)); ;

            foreach (GameObject mob in mobs)
            {
                mob.SendMessage("HandleSelection", area);
            }

            dragStartPos = Vector2.zero;
            dragEndPosition = Vector2.zero;
        }


    }

    float speed = 7f;

    void handleKeys()
    {
        Camera.main.transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));

    }



    void drawSelectionBox()
    {
        if (dragStartPos != Vector2.zero && dragEndPosition != Vector2.zero)
        {
            Rect tmp = new Rect(dragStartPos.x, Screen.height - dragStartPos.y, dragEndPosition.x - dragStartPos.x, -1 * ((Screen.height - dragStartPos.y) - (Screen.height - dragEndPosition.y)));
            GUI.DrawTextureWithTexCoords(tmp, tex, new Rect(dragStartPos.x, Screen.height - dragStartPos.y, dragEndPosition.x - dragStartPos.x, -1 * ((Screen.height - dragStartPos.y) - (Screen.height - dragEndPosition.y))));
        }
    }
    
    void handleScrollUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            Camera.main.orthographicSize++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Camera.main.orthographicSize--;
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);
    }

}
