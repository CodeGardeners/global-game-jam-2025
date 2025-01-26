using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private Queue<Character> remainingCharacters = new();

    public void Awake(){
        System.Random random = new();
        var shuffle = Enumerable.Range(0,transform.childCount).OrderBy(x => random.Next());
        foreach (int i in shuffle)
        {
            if(!transform.GetChild(i).TryGetComponent<Character>(out var character))
            {
                continue;
            }
            remainingCharacters.Enqueue(character);
        }
    }

    public Character SpawnNew(){
        if (remainingCharacters.Count == 0){
            return null;
        }
        Character character = remainingCharacters.Dequeue();
        character.gameObject.SetActive(true);
        var director = character.transform.GetChild(1).GetComponent<Audio.AudioGuestCharacter>().playableDirector;
        Debug.Log("Spawn Character of " + director.name);
        return character;
    }
}
