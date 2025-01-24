using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject characterPrefab;
    
    [SerializeField]
    private Transform spawnPoint;

    public GameObject SpawnNew(){
        var go = Instantiate(characterPrefab);
        go.transform.position = spawnPoint.position;
        return go;
    }
}
