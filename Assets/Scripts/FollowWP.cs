
using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        // Sem caminho ou já chegou ao fim: não faz nada
        if (g.pathList.Count == 0 || currentWP == g.pathList.Count)
            return;

        // Chegou ao waypoint atual? Passa ao seguinte
        if (Vector3.Distance(
                g.pathList[currentWP].getId().transform.position,
                this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }

        // Ainda há waypoints: roda e move-se em direção ao próximo
        if (currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getId().transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x,
                                             this.transform.position.y,
                                             goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(
                this.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed);

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}