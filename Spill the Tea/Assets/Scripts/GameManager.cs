using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly Color[] Colors = {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow
    };

    [SerializeField]
    private Counter counter;

    [SerializeField]
    private List<Table> tables;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    private Dictionary<Character, Table> characters;

    public void Awake()
    {
        characters = new Dictionary<Character, Table>();
    }

    public void Start()
    {
        counter.SetToTableAction(ToTable);
        for (int i = 0; i < tables.Count; i++)
        {
            tables[i].SetColor(Colors[i]);
        }
    }

    public void Update(){
        if(characters.Count >= 16){
            return;
        }

        if(counter.GetWaitingCount == 0){
            AddCharacter();
        }
    }

    public void AddCharacter()
    {
        var character = characterSpawner.SpawnNew();
        character.SetToCounterAction(ToCounter);
        characters.Add(character, null);
        counter.AddCharacter(character);
    }

    private void ToCounter(Character character)
    {
        var table = characters[character];
        characters[character] = null;
        table.UnseatCharacter(character);
        counter.AddCharacter(character);
    }

    private void ToTable(Character character, int i)
    {
        var table = tables[i];
        table.SeatCharacter(character);
        characters[character] = table;
    }
}
