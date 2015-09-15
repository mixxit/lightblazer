using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    public Transform tilePrefab;

    private int maxrenderx = 128;
    private int maxrendery = 128;

    private int centerpointx = 0;
    private int centerpointy = 0;

    // Use this for initialization
    void Start () {
        //Load prefab "Grass" from "Resources/Prefabs/" folder.
        tilePrefab = Resources.Load<Transform>("Tiles/Grass1");

        //If we can't find the prefab then log a warning.
        if (!tilePrefab)
            Debug.LogWarning("Unable to find TilePrefab in your Resources folder.");

        // Start at half the minimum size


        //Iterate over each future tile positions for x and y
        for (int y = 0-(maxrendery/2); y < (maxrendery / 2); y++)
        {
            for (int x = 0 - (maxrenderx / 2); x < (maxrenderx / 2); x++)
            {
                //Instantiate tile prefab at the desired position as a Transform object
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                //Set the tiles parent to the GameObject this script is attached to
                tile.parent = transform;
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
