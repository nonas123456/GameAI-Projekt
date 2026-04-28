using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VectorGraphics;
using UnityEditor;

public class MissileMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject currentTile;
    private RaycastHit hit;
    public PlayerScript PlayerScript;
    [SerializeField] private GameObject CurrentPlayerTile;
    private GameObject previousPlayerTile;
    
    private Stack<AstarNode> path;
    private int speed = 10;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit);
        if (hit.collider != null)
        {
            currentTile = hit.collider.gameObject;
            CurrentPlayerTile = PlayerScript.CurrentTile;

            if (previousPlayerTile != CurrentPlayerTile)
            {
                path = Movement(currentTile.GetComponent<GridNode>(), CurrentPlayerTile.GetComponent<GridNode>());
                previousPlayerTile = CurrentPlayerTile;
            }
        }
    }

    private void LateUpdate()
    {
        if (path != null && path.Count > 0)
        {
            Vector3 moveTo = path.Peek().node.transform.position;
            moveTo.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * speed);
            transform.LookAt(moveTo);
            print(transform.rotation.eulerAngles);
            if (transform.position == moveTo)
            {
                path.Pop();
            }
        }

        if (path.Count <= 0)
        {
            print("Destination reached");
        }
    }

    private float Heuristic(GridNode current, GridNode target, Vector3 targetDirection, Vector3 currentDirection)
    { 
        float penalty;
       float distance = Vector3.Distance(current.gameObject.transform.position, target.gameObject.transform.position);
       if (currentDirection == targetDirection)
       {
           penalty = 0;
       }
       else if (currentDirection == (-1 * targetDirection))
       {
           penalty = 1;
       }
       else
       {
           penalty = 0.5f;
       }
       
       return distance + (penalty * 10);
    }
    
    private class AstarNode
    {
        public float g;
        public float h;
        public AstarNode parent;
        public GridNode node;
        public Vector3 direction;

        public AstarNode(float g, float h, AstarNode parent, GridNode node, Vector3 direction)
        {
            this.g = g;
            this.h = h;
            this.parent = parent;
            this.node = node;
            this.direction = direction;   
        }

    }

    private Stack<AstarNode> Movement(GridNode start, GridNode target)
    {
        AstarNode current;
        
        List<AstarNode> queue = new List<AstarNode>();
        
        queue.Add(new AstarNode(0, Heuristic(start, target, gameObject.transform.forward, gameObject.transform.forward), null, start, gameObject.transform.forward));
        
        while (queue.Count > 0)
        {
            current = queue[0];
            queue.RemoveAt(0);

            if (current.node == target)
            {
                Stack<AstarNode> stack = new Stack<AstarNode>();
                while (current.parent != null)
                {
                    stack.Push(current);
                    print("Node Pushed " + current.node);
                    current = current.parent;
                }
                return stack;
            }
            
            foreach (GridNode neighbor in current.node.neighbors)
            {
                AstarNode neighborAstar = IsPresent(neighbor, queue);
                if (neighborAstar == null)
                {
                    Vector3 neighborDirection = (neighbor.gameObject.transform.position - current.node.gameObject.transform.position).normalized;
                    neighborAstar = new AstarNode(current.g + Heuristic(neighbor, current.node, neighborDirection, current.direction), Heuristic(neighbor, target, neighborDirection, current.direction), current, neighbor, neighborDirection );
                    queue.Add(neighborAstar);
                }
                else
                {
                    if ((current.g + Heuristic(neighbor, current.node, neighborAstar.direction, current.direction)) < neighborAstar.g)
                    {
                      neighborAstar.parent = current;
                      neighborAstar.direction = (neighbor.gameObject.transform.position - current.node.gameObject.transform.position).normalized;
                      neighborAstar.g = current.g + Heuristic(neighbor, current.node, neighborAstar.direction, current.direction);
                    }
                }
            }
            queue.Sort((node1, node2) => (node1.h + node1.g).CompareTo(node2.h + node2.g));
        }
        return null;
    }

    private AstarNode IsPresent(GridNode node, List<AstarNode> queue)
    {
        foreach (AstarNode queueNode in queue)
        {
            if (node == queueNode.node)
            {
                return queueNode;
            }
        }
        return null;
    }
    
}
