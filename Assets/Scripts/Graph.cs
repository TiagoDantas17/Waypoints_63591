
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    List<Edge> edges = new List<Edge>();
    List<node> nodes = new List<node>();
    List<node> pathList = new List<node>();

    public Graph() { }

    public void AddNote(GameObject id)
    {
        node newNode = new node(id);
        nodes.Add(newNode);
    }

    public void AddNode(GameObject fromNode, GameObject toNode)
    {
        node from = Findnode(fromNode);
        node to = Findnode(toNode);

        if (from != null && to != null)
        {
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }

    node Findnode(GameObject id)
    {
        foreach (node n in nodes)
        {
            if (n.getId() == id)
                return n;
        }

        return null;
    }
}