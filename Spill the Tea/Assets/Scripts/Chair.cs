using UnityEngine;
using UnityEngine.AI;

public class Chair : MonoBehaviour
{
    private NavMeshObstacle navObstacle;

    void Start()
    {
        navObstacle = GetComponent<NavMeshObstacle>();
    }

    public void DisableObstacle()
    {
        navObstacle.enabled = false;
    }

    public void EnableObstacle()
    {
        navObstacle.enabled = true;
    }
}
