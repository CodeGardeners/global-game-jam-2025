using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private Character characterPrefab;
    
    [SerializeField]
    private Transform spawnPoint;

    public Character SpawnNew(){
        var go = Instantiate(characterPrefab);
        go.transform.position = spawnPoint.position;
        return go;
    }
}
