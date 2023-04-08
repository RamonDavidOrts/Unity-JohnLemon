using UnityEngine;
using UnityEngine.AI;

public class WayPointPatrol : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform[] waypoints;

    int waypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[waypointIndex].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        }
    }
}
