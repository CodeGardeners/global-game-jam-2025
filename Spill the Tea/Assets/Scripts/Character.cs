using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Action navigationEnded;
    private bool isNavigating;

    public void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (isNavigating && !navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    isNavigating = false;
                    navigationEnded();
                }
            }
        }
    }

    public void SetTarget(Transform target, Action navigationEnded)
    {
        this.navigationEnded = navigationEnded;
        navAgent.SetDestination(target.position);
        isNavigating = true;
    }
}
