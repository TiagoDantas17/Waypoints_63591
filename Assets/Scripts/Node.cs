using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class node : MonoBehaviour
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    GameObject id;
    public float xPos; 
    public float yPos; 
    public float zPos;

    public float f, g, h; public Node cameFrom;
    public node(GameObject i)
    {
        id = i;
        xPos = i.transform.position.x;
        yPos = i.transform.position.y;
        zPos = i.transform.position.z;
        path = null;
    }
    public GameObject getId()
    {
        return id;
    }
}