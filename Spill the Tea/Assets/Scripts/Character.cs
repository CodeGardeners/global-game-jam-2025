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
    private Action<Character> toCounter;
    private Action navigationEnded;
    private CharacterState characterState;
    private bool isNavigating;

    private int title;
    private int track;

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

    public int GetTitle()
    {
        return title;
    }

    public void SetIdentity(int title, int track)
    {
        this.title = title;
        this.track = track;
    }

    public void SetToCounterAction(Action<Character> toCounter)
    {
        this.toCounter = toCounter;
    }

    public void SetTarget(Vector3 target, CharacterState characterState, Action navigationEnded = null)
    {
        this.characterState = characterState;
        this.navigationEnded = navigationEnded;
        navAgent.SetDestination(target);
        isNavigating = true;
        Debug.Log("Character " + title + " " + track + " is navigating to " + target);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (characterState is not CharacterState.Table)
        {
            return;
        }
        toCounter(this);
    }
}
