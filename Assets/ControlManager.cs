using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    const int orthographicSizeMin = 10;
    const int orthographicSizeMax = 20;

    private Vector3 startDragPos;
    private Vector3 endDragPos;
    private GameObject selectionbox;

    // Use this for initialization
    void Start() {
        Camera.main.orthographicSize = 10;
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
            Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = newpos.x - startDragPos.x;
            float y = newpos.y - startDragPos.y;

            float midpointx = startDragPos.x + (x/2);
            float midpointy = startDragPos.y + (y/2);

            selectionbox.transform.position = new Vector3(midpointx, midpointy, 0);
            selectionbox.transform.localScale = new Vector3(x, y, 0.1f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning("Mouse is down");
            if (selectionbox == null)
            {
                startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectionbox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                selectionbox.transform.position = new Vector3(startDragPos.x, startDragPos.y, 0);  ;
                selectionbox.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Color colour = Color.blue;
                colour.a = 0.35f;
                selectionbox.GetComponent<Renderer>().material.color = colour;
                selectionbox.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Destroy(selectionbox);
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
