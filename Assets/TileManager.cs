using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    public Transform grassPrefab;
    public Transform rockPrefab;
    private int maxrenderx = 128;
    private int maxrendery = 128;

    private int centerpointx = 0;
    private int centerpointy = 0;

    // Use this for initialization
    void Start () {
        //Load prefab "Grass" from "Resources/Prefabs/" folder.
        grassPrefab = Resources.Load<Transform>("Tiles/Grass1");
        rockPrefab = Resources.Load<Transform>("Tiles/Rocks1");


        //If we can't find the prefab then log a warning.
        if (!grassPrefab)
            Debug.LogWarning("Unable to find grassPrefab in your Resources folder.");

        if (!rockPrefab)
            Debug.LogWarning("Unable to find rockPrefab in your Resources folder.");

        // Start at half the minimum size


        //Iterate over each future tile positions for x and y
        for (int y = 0-(maxrendery/2); y < (maxrendery / 2); y++)
        {
            for (int x = 0 - (maxrenderx / 2); x < (maxrenderx / 2); x++)
            {
                //Instantiate tile prefab at the desired position as a Transform object
                Transform grasstile = Instantiate(grassPrefab, new Vector2(x, y), Quaternion.identity) as Transform;
                //Set the tiles parent to the GameObject this script is attached to
                grasstile.parent = transform;

                int rand = Random.Range(1, 10);
                if (rand > 7)
                {
                    Debug.LogWarning("Chance of rock");
                    Transform rocktile = Instantiate(rockPrefab, new Vector2(x, y), Quaternion.identity) as Transform;

                    //Set the tiles parent to the GameObject this script is attached to
                    rocktile.parent = transform;
                }

            }
        }
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
