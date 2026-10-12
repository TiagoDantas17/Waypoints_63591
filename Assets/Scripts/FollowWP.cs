
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public enum FollowMode
    {
        Waypoints,
        Tracker
    }

    [Header("Modo de movimento")]
    public FollowMode mode = FollowMode.Waypoints;

    [Header("Waypoints do circuito")]
    public GameObject[] waypoints;

    [Header("Tracker a seguir")]
    public GameObject tracker;

    [Header("Movimento")]
    public float speed = 10.0f;
    public float rotSpeed = 10.0f;

    [Header("Distâncias")]
    public float waypointDistance = 1.5f;
    public float followDistance = 4.0f;
    public float sideOffset = 0.0f;
    public float stopDistance = 0.5f;

    private int currentWP = 0;

    void Update()
    {
        if (mode == FollowMode.Waypoints)
        {
            FollowWaypoints();
        }
        else
        {
            FollowTracker();
        }
    }

    // MODO 1: Percorrer os waypoints do circuito
    void FollowWaypoints()
    {
        if (!FindValidWaypoint())
            return;

        Vector3 target =
            waypoints[currentWP].transform.position;

        target.y = transform.position.y;

        // Verifica se chegou ao waypoint atual
        if (Vector3.Distance(transform.position, target)
            < waypointDistance)
        {
            // Passa ao próximo waypoint
            currentWP = (currentWP + 1) % waypoints.Length;

            if (!FindValidWaypoint())
                return;

            target = waypoints[currentWP].transform.position;
            target.y = transform.position.y;
        }

        MoveTowards(target, false);
    }

    // Ignora waypoints vazios ou não atribuídos
    bool FindValidWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return false;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[currentWP] != null)
                return true;

            currentWP = (currentWP + 1) % waypoints.Length;
        }

        return false;
    }

    // MODO 2: Seguir o Tracker
    void FollowTracker()
    {
        if (tracker == null)
            return;

        // Define um ponto atrás do Tracker
        Vector3 target =
            tracker.transform.position
            - tracker.transform.forward * followDistance
            + tracker.transform.right * sideOffset;

        // Mantém o movimento no plano horizontal
        target.y = transform.position.y;

        MoveTowards(target, true);
    }

    // Rotação suave e movimento para o destino
    void MoveTowards(Vector3 target, bool stopNearTarget)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance < 0.05f)
            return;

        // No modo Tracker, mantém alguma distância
        if (stopNearTarget && distance <= stopDistance)
            return;

        // Calcula a direção do movimento
        Quaternion lookatTarget =
            Quaternion.LookRotation(direction);

        // Roda suavemente em direção ao destino
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookatTarget,
            rotSpeed * Time.deltaTime
        );

        // Avança para a frente
        transform.Translate(
            0f,
            0f,
            speed * Time.deltaTime
        );
    }
}