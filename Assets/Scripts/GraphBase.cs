using UnityEngine;

public class GraphBase
{
    public bool AStar(GameObject startId, GameObject endId)
    {
        Node start = FindNode(startId);
        Node end = FindNode(endId);

        if (start == null || end == null)
        {
            return false;
        }

        // TODO: Implementar a lógica do A* aqui

        return true;
    }

    // Método auxiliar para encontrar o componente Node no GameObject
    public Node FindNode(GameObject nodeObj)
    {
        if (nodeObj == null) return null;
        return nodeObj.GetComponent<Node>();
    }
}