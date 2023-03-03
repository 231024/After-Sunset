using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public float moveSpeed;

    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
            dir.z = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.z = 1.0f;
        if (Input.GetKey(KeyCode.UpArrow))
            dir.x = -1.0f;
        if (Input.GetKey(KeyCode.DownArrow))
            dir.x = 1.0f;
        navMeshAgent.velocity = dir.normalized * moveSpeed;
    }
}
