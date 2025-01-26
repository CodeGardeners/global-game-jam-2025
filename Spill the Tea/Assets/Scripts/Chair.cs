using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SpriteRenderer))]
public class Chair : MonoBehaviour
{
    private NavMeshObstacle navObstacle;
    private new SpriteRenderer renderer;

    [SerializeField]
    private List<Sprite> chairSprites;
    
    [SerializeField]
    private bool bottomChair = false;
    public void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
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

    public void SetTableType(Table.TableType type)
    {
        renderer.sprite = chairSprites[(int)type];
    }

    public bool IsBottomChair()
    {
        return bottomChair;
    }
}
