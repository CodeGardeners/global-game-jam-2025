using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Texture2D> characterTextures;

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
            SpriteWobble wobble = character.GetComponentInChildren<SpriteWobble>(true);
            if (characterTextures.Count != 0){
                wobble.characterTexture = characterTextures[character.characterId % characterTextures.Count];
                wobble.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 0.2f, 0.4f, 0.9f, 0.95f, 1f, 1f);
            }else {
                Debug.LogWarning("No character textures found");
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
