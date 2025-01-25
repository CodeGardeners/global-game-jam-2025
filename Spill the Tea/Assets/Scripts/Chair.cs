using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Renderer))]
public class Chair : MonoBehaviour
{
    private NavMeshObstacle navObstacle;
    private new Renderer renderer;


    public void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

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

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
