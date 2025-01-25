using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private List<Transform> chairs;

    private Dictionary<Transform, Character> seatedCharacters;

    public void Awake(){
        seatedCharacters = new Dictionary<Transform, Character>();
    }

    public Transform GetNextAvailableChair(){
        return chairs.Where(x => !seatedCharacters.Keys.Contains(x)).FirstOrDefault();
    }

    public Transform SeatCharacter(Character character){
        var transform = GetNextAvailableChair();
        seatedCharacters.Add(transform, character);
        return transform;
    }
}
