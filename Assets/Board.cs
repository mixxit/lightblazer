using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject emptyTile;
    public GameObject[] terrainPrefabs;
    public Cell[,] board;

    void Awake()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        board = new Cell[width, height];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                GameObject cellPrefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Length)];

                GameObject cell = GameObject.Instantiate(
                  cellPrefab,
                  new Vector3(x, y, 0),
                  Quaternion.identity) as GameObject;
                cell.transform.parent = transform;
                Cell c = cell.GetComponent<Cell>();
                c.coordinates = new Vector2(x, y);
                board[x, y] = c;
            }
    }

    public void RemoveCell(Cell cell)
    {
        GameObject newcell = GameObject.Instantiate(
                  emptyTile,
                  new Vector3(cell.coordinates.x, cell.coordinates.y, 0),
                  Quaternion.identity) as GameObject;
        newcell.transform.parent = transform;
        Cell c = newcell.GetComponent<Cell>();
        c.coordinates = new Vector2(cell.coordinates.x, cell.coordinates.y);
        board[(int)cell.coordinates.x, (int)cell.coordinates.y] = c;
        Destroy(cell.gameObject);
    }

    public Cell GetRandomWalkableCell()
    {

        bool found = false;
        while (found == false)
        {
            int values = board.GetLength(0) * board.GetLength(1);
            int index = Random.Range(0, values);
            Cell target = board[index / board.GetLength(0), index % board.GetLength(0)];
            if (target.IsWalkable())
            {
                return target;
            }
        }

        return null;


    }


    protected List<Cell> FindPath(Cell origin, Cell goal)
    {
        AStar pathFinder = new AStar();
        pathFinder.FindPath(origin, goal, board, false);
        return pathFinder.CellsFromPath();
    }
}