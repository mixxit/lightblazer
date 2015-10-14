using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkController : MonoBehaviour {

    List<Chunk> loadedChunks = new List<Chunk>();
    public float chunksize = 32;
    public float chunkloaddistance = 1;

	// Use this for initialization
	void Start () {
        Vector2 worldpoint = new Vector2(0, 0);
        loadedChunks = GetChunksAtWorldPoint(worldpoint);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Chunk> GetChunksAtWorldPoint(Vector2 worldpoint)
    {
        List<Chunk> chunklist = new List<Chunk>();

        Vector2 chunkpoint = GetChunkPointFromWorldPoint(worldpoint);
        chunklist = GetChunksAtChunkPoint(chunkpoint);

        return chunklist;
    }

    public Vector2 GetChunkPointFromWorldPoint(Vector2 worldpoint)
    {
        return new Vector2(worldpoint.x / chunksize, worldpoint.y / chunksize);
    }

    public List<Chunk> GetChunksAtChunkPoint(Vector2 chunkpoint)
    {
        List<Chunk> chunks = new List<Chunk>();

        float minx = chunkpoint.x - chunkloaddistance;
        float miny = chunkpoint.y - chunkloaddistance;
        float maxx = chunkpoint.x + chunkloaddistance;
        float maxy = chunkpoint.y + chunkloaddistance;

        float curx = minx;
        float cury = miny;

        for (float x = minx; x <= maxx; x++ )
        {
            for(float y = miny; y <= maxy; y++)
            {
                Chunk curchunk = new Chunk(this);
                curchunk.RandomiseCells();
                curchunk.chunkposition = new Vector2(x, y);

                chunks.Add(curchunk);
            }
        }


        return chunks;
    }

}
