using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            navAgent.SetDestination(target.position);
        }
    }
}
