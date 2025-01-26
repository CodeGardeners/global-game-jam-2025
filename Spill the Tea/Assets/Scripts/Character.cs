using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;
using Audio;

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

    public DialogAssociator dialogAssociator;
    public int characterId;

    public MeshRenderer meshRendererOfSprite;
    private int defaultRenderQueue;
    public int bottomChairRenderQueue  =3000;
    public void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // get default renderQueue;
        defaultRenderQueue = meshRendererOfSprite.material.renderQueue;
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

    public AudioGuestCharacter GetAudioGuestCharacter(){
        foreach (Transform child in transform){
            if (child.gameObject.GetComponent<AudioGuestCharacter>() != null){
                return child.gameObject.GetComponent<AudioGuestCharacter>();
            }
        }
        return null;
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

    public TextAsset GetInkDialogue()
    {
        return inkDialogue;
    }

    public void SetRendererToBottomChairPosition()
    {
        meshRendererOfSprite.material.renderQueue = bottomChairRenderQueue;
    }
    
    public void ResetRendererToBottomChairPosition()
    {
        meshRendererOfSprite.material.renderQueue = defaultRenderQueue;
    }
}
