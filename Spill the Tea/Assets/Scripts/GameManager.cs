using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Counter counter;

    [SerializeField]
    private List<Table> tables;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    private List<Character> characters;

    public void Awake()
    {
        characters = new List<Character>();
    }

    public void Start()
    {
        this.GameLoop();
    }

    public void GameLoop()
    {
        if(characters.Count >= 16){
            return;
        }
        
        var character = characterSpawner.SpawnNew();
        characters.Add(character);
        character.SetTarget(counter.GetPosition(), async () =>
        {
            var table = await counter.ShowSelection();
            var nextTarget = tables[table].SeatCharacter(character);
            character.SetTarget(nextTarget, () => this.GameLoop());
        });
    }
}
