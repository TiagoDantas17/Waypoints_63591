
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UN, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoint in waypoints)
            {
                graph.AddNode(waypoint);
            }

            foreach (Link link in links)
            {
                graph.AddEdge(link.node1, link.node2);
                if (link.dir == Link.direction.BI)
                {
                    graph.AddEdge(link.node2, link.node1);
                }
            }
        }
    }

    void Update()
    {
    }
}