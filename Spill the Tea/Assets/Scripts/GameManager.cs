using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly Color[] COLORS = {
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

    private List<Character> characters;

    public static Color[] getColors()
    {
        return COLORS;
    }

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
        for (int i = 0; i < tables.Count; i++)
        {
            tables[i].SetColor(COLORS[i]);
        }
    }
}
