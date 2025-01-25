using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    [SerializeField]
    private Character characterPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private List<int> remainingCharacters;

    public void Awake(){
        remainingCharacters = Enumerable.Range(0, 16).ToList();
        System.Random random = new System.Random();
        remainingCharacters = remainingCharacters.OrderBy(x => random.Next()).ToList();
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
