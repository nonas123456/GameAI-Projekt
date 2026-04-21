using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    public int gridX;
    public int gridZ;
    
    
    
    public List<GridNode> neighbors = new List<GridNode>();

    public GridNode(int x, int z)
    {
        gridX = x;
        gridZ = z;
    }
    
}
