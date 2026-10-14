using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;      // waypoints, por ordem
    int currentWP = 0;

    public float speed = 10.0f;         // velocidade do tanque
    public float rotSpeed = 10.0f;      // velocidade de rotação
    public float lookAhead = 10.0f;     // distância máxima a que o tracker fica à frente

    GameObject tracker;                 // objeto invisível que "puxa" o tanque

    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;   // põe a true para o veres

        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }

    void ProgressTracker()
    {
        // Se o tracker já vai longe demais à frente, espera pelo tanque
        if (Vector3.Distance(tracker.transform.position, this.transform.position) > lookAhead)
            return;

        // Chegou ao waypoint? Passa ao seguinte
        if (Vector3.Distance(tracker.transform.position,
                             waypoints[currentWP].transform.position) < 3.0f)
            currentWP++;

        // Chegou ao fim da lista: volta ao início
        if (currentWP >= waypoints.Length)
            currentWP = 0;

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 20) * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("FollowWP: o array de waypoints está vazio!", this);
            return;
        }

        ProgressTracker();

        // O tanque olha para o tracker, não diretamente para o waypoint
        Quaternion lookAtWP = Quaternion.LookRotation(
            tracker.transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}