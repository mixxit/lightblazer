using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    const int orthographicSizeMin = 10;
    const int orthographicSizeMax = 20;

    private Vector2 startDragPos;
    private Vector2 endDragPos;
    private Transform selectionbox;
    private Transform selectionbox_prefab;

    // Use this for initialization
    void Start() {
        Camera.main.orthographicSize = 10;
        selectionbox_prefab = Resources.Load<Transform>("Sprites/selectionbox");
    }

    // Update is called once per frame
    void Update() {
        handleScrollUpdate();
        handleDragSelect();
    }

    void handleDragSelect()
    {
        if (selectionbox != null)
        {
            Vector2 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = newpos.x - startDragPos.x;
            float y = newpos.y - startDragPos.y;

            float midpointx = startDragPos.x + (x/2);
            float midpointy = startDragPos.y + (y/2);

            selectionbox.transform.position = new Vector2(midpointx, midpointy);
            selectionbox.transform.localScale = new Vector2(x, y);
            selectionbox.GetComponent<SpriteRenderer>().bounds.size.x = x;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning("Mouse is down");
            if (selectionbox == null)
            {
                startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectionbox = Instantiate(selectionbox_prefab, new Vector2(0, 0), Quaternion.identity) as Transform;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Destroy(selectionbox.gameObject);

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
