using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
    private Action<Character> navigationEnded;
    private CharacterState characterState;
    private bool isNavigating;

    private bool isLocked = false;

    public TextAsset inkDialogue;

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
                    navigationEnded?.Invoke(this);
                }
            }
        }
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void SetTarget(Vector3 target, CharacterState characterState, Action<Character> navigationEnded = null)
    {
        this.characterState = characterState;
        this.navigationEnded = navigationEnded;
        navAgent.SetDestination(target);
        isNavigating = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (characterState is not CharacterState.Table || isLocked)
        {
            return;
        }

        // TODO check if within dialog, then you can't click characters
        GameManager.Instance.ToCounter(this);

    }
}
