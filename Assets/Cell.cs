using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public Vector2 coordinates { get; set; }
    public virtual bool IsWalkable()
    {
        return true;
    }
    public virtual float MovementCost()
    {
        return 0;
    }
}