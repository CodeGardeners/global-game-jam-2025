using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject counter;
    
    [SerializeField]
    private List<Table> tables;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    private List<GameObject> characters;

    public void Awake(){
        characters = new List<GameObject>();
        var character = characterSpawner.SpawnNew();
        characters.Add(character);
    }
}
