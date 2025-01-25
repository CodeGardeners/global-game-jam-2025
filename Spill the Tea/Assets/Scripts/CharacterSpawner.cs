using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    [SerializeField]
    private Character characterPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private List<int> remainingCharacters;

    public void Awake(){
        var range = Enumerable.Range(0, 16);
        System.Random random = new();
        remainingCharacters = range.OrderBy(x => random.Next()).ToList();
    }

    public Character SpawnNew(){
        var go = Instantiate(characterPrefab);
        go.transform.position = spawnPoint.position;
        int character = remainingCharacters.First();
        remainingCharacters.RemoveAt(0);
        int title = character / 4;
        int track = character % 4;
        go.SetIdentity(title, track);
        return go;
    }
}
