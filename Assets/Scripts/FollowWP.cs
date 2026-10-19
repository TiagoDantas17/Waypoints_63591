using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public WPManager wpManager;
    public float speed = 10f;
    public float rotSpeed = 5f;
    public float accuracy = 1f;

    GameObject[] wps;
    int currentWP = 0;

    void Start()
    {
        if (wpManager == null)
            wpManager = FindFirstObjectByType<WPManager>();

        if (wpManager == null)
        {
            Debug.LogError("Não existe nenhum objeto com o script WPManager na cena.", this);
            enabled = false;
            return;
        }

        wps = wpManager.waypoints;

        if (wps == null || wps.Length == 0)
        {
            Debug.LogError("O WPManager não tem waypoints no array.", this);
            enabled = false;
        }
    }

    void LateUpdate()
    {
        Vector3 target = wps[currentWP].transform.position;
        target.y = transform.position.y;

        if (Vector3.Distance(transform.position, target) < accuracy)
            currentWP = (currentWP + 1) % wps.Length;

        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, rotSpeed * Time.deltaTime);
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}