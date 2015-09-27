using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public bool walkable = true;
    public Vector2 coordinates { get; set; }
    public virtual bool IsWalkable()
    {
        return walkable;
    }
    
    public virtual float MovementCost()
    {
        return 0;
    }
}