using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        characters = new Dictionary<Character, Table>();
    }

    public void Start()
    {
        for (int i = 0; i < tables.Count; i++)
        {
            tables[i].SetColor(Colors[i]);
        }
    }

    public void Update()
    {
        if (characters.Count >= 16)
        {
            return;
        }

        if (counter.GetWaitingCount == 0)
        {
            AddCharacter();
        }
    }

    public void AddCharacter()
    {
        var character = characterSpawner.SpawnNew();
        if (character == null)
        {
            Debug.LogError("No more characters to spawn");
            return;
        }
        characters.Add(character, null);
        counter.AddCharacter(character);
    }

    public void ToCounter(Character character)
    {
        var table = characters[character];
        characters[character] = null;
        table.UnseatCharacter(character);
        counter.AddCharacter(character);
        counter.EnableButton(tables.IndexOf(table));
    }

    public void ToTable(Character character, int i)
    {
        var table = tables[i];
        if(table.SeatCharacter(character)){
            counter.DisableButton(i);
        }
        characters[character] = table;
    }
}
