using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private List<Button> buttons;

    [SerializeField]
    private Transform counterPosition;

    [SerializeField]
    private Vector3 offset;

    private Queue<Character> waitingCharacters;

    public int GetWaitingCount => waitingCharacters.Count;

    public void Awake(){
        waitingCharacters = new Queue<Character>();
    }

    void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            int colorIndex = i % GameManager.Colors.Length;
            button.GetComponent<Image>().color = GameManager.Colors[colorIndex];
            var j = i; // avoid closure problem
            button.onClick.AddListener(() => OnClick(j));
        }
    }

    public void AddCharacter(Character character){
        character.SetTarget(counterPosition.position + waitingCharacters.Count * offset, Character.CharacterState.ToCounter, waitingCharacters.Count == 0? CounterReached : null);
        waitingCharacters.Enqueue(character);
    }

    public void OnClick(int num)
    {
        ui.SetActive(false);
        GameManager.Instance.ToTable(waitingCharacters.Dequeue(), num);

        if(waitingCharacters.Count != 0){
            ResetPosition();
        }
    }

    private void ResetPosition(){
        var array = waitingCharacters.ToArray();
        for (int i = array.Length-1; i >= 0; i--)
        {
            array[i].SetTarget(counterPosition.position + i * offset, Character.CharacterState.ToCounter, i == 0? CounterReached : null);
        }
    }

    private void CounterReached(){
        // TODO disable buttons when table full
        ui.SetActive(true);
    }
}
