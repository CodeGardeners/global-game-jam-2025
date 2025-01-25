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
        var shuffle = Enumerable.Range(0,transform.childCount); //.OrderBy(x => random.Next());
        foreach (int i in shuffle)
        {
            var character = transform.GetChild(i).GetComponent<Character>();
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
