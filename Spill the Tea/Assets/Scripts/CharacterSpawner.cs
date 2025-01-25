using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    [SerializeField]
    private Character characterPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private Queue<Character> remainingCharacters = new();

    public void Awake(){
        System.Random random = new();
        foreach (Transform child in transform)
        {
            var character = child.GetComponent<Character>();
            remainingCharacters.Enqueue(character);
        }
    }

    public Character SpawnNew(){
        if (remainingCharacters.Count == 0){
            return null;
        }
        Character character = remainingCharacters.Dequeue();
        character.gameObject.SetActive(true);
        return character;
    }
}
