using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Counter counter;

    [SerializeField]
    private GameObject endScreen;

    [SerializeField]
    private List<Table> tables;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    private Dictionary<Character, Table> characters;

    private int LockedTables = 0;

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

    public void AddCharacter()
    {
        var character = characterSpawner.SpawnNew();
        if (character == null)
        {
            Debug.LogWarning("No more characters to spawn");
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

    public void addLockedTable(){
        LockedTables++;
        if(LockedTables == tables.Count){
            Debug.Log("All tables are locked, calling end screen");
            endScreen.SetActive(true);
        }
    }
}
