
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;

    private int currentWP = 0;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float waypointDistance = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        // Verifica se existem waypoints
        if (waypoints == null || waypoints.Length == 0)
            return;

        if (waypoints[currentWP] == null)
            return;

        // Avança para o próximo waypoint quando estiver suficientemente perto
        if (Vector3.Distance(
            transform.position,
            waypoints[currentWP].transform.position) < waypointDistance)
        {
            currentWP++;

            // Quando chega ao último waypoint, volta ao primeiro
            if (currentWP >= waypoints.Length)
                currentWP = 0;
        }

        if (waypoints[currentWP] == null)
            return;

        // Calcula a direção para o próximo waypoint
        Vector3 direction =
            waypoints[currentWP].transform.position - transform.position;

        // Mantém a rotação horizontal
        direction.y = 0f;

        // Roda suavemente na direção do movimento
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookatWP = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookatWP,
                rotSpeed * Time.deltaTime
            );
        }

        // Avança na direção para a qual o objeto está virado
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}