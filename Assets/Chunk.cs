using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {
    public Vector2 chunkposition = new Vector2();

    public List<Cell> cells = new List<Cell>();
    private ChunkController parent;

    public Chunk(ChunkController chunkController)
    {
        this.parent = chunkController;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomiseCells()
    {
        cells.Clear();

        for (float x = 0; x <= parent.chunksize; x++)
        {
            for (float y = 0; y <= parent.chunksize; y++)
            {
                Cell newcell = new Cell();
                newcell.coordinates = new Vector2(x, y);

                cells.Add(newcell);
            }

        }
    }
}
