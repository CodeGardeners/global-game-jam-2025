using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour, IPointerClickHandler
{
    public enum CharacterState
    {
        ToCounter,
        Counter,
        ToTable,
        Table
    }

    private NavMeshAgent navAgent;
    private Action navigationEnded;
    private CharacterState characterState;
    private bool isNavigating;

    public int Title { get; private set; }
    public int Track { get; private set; }

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
                    if (characterState is CharacterState.ToCounter)
                    {
                        characterState = CharacterState.Counter;
                    }
                    else if (characterState is CharacterState.ToTable)
                    {
                        characterState = CharacterState.Table;
                    }
                    isNavigating = false;
                    navigationEnded?.Invoke();
                }
            }
        }
    }


    public void SetIdentity(int title, int track)
    {
        Title = title;
        Track = track;
    }

    public void SetTarget(Vector3 target, CharacterState characterState, Action navigationEnded = null)
    {
        this.characterState = characterState;
        this.navigationEnded = navigationEnded;
        navAgent.SetDestination(target);
        isNavigating = true;
        Debug.Log("Character " + Title + " " + Track + " is navigating to " + target);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (characterState is not CharacterState.Table)
        {
            return;
        }

        // TODO check if within dialog, then you can't click characters
        GameManager.Instance.ToCounter(this);

    }
}
