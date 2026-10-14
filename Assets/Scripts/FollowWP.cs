using UnityEngine;

public class FollowWP : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        if (wpManager == null)
        {
            Debug.LogError("Wp Manager não está atribuído no Inspector.", this);
            enabled = false; // evita o spam de erros no LateUpdate
            return;
        }

        WPManager manager = wpManager.GetComponent<WPManager>();
        if (manager == null)
        {
            Debug.LogError("O objeto atribuído em Wp Manager não tem o componente WPManager.", this);
            enabled = false;
            return;
        }

        wps = manager.waypoints;
        g = manager.graph;

        if (wps == null || wps.Length == 0 || g == null)
        {
            Debug.LogError("WPManager sem waypoints ou sem grafo. Verifica a lista de waypoints.", this);
            enabled = false;
            return;
        }

        currentNode = wps[0];
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[5]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[14]);
        currentWP = 0;
    }

    public void GoToFactory()
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        if (g == null || g.pathList.Count == 0 || currentWP == g.pathList.Count)
            return;

        if (Vector3.Distance(g.pathList[currentWP].getId().transform.position, this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }

        if (currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            if (direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            }

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}