using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;
    
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        //navMeshAgent.SetDestination(player.transform.position);
    }
}
