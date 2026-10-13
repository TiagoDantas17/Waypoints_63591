
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    public List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if (from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edgeList.Add(edge);
        }
    }

    Node FindNode(GameObject id)
    {
        foreach (Node n in nodes)
        {
            if (n.getId() == id)
                return n;
        }

        return null;
    }

    public bool AStar(GameObject starId, GameObject endId)
    {
        Node start = FindNode(starId);
        Node end = FindNode(endId);

        if (start == null || end == null)
        {
            return false;
        }