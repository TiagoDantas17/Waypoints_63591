using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public node startNode;
    public node endNode;
    public Edge(node from, node to)
    {
        startNode = from;
        endNode = to;
    }
}


