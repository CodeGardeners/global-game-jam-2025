using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private List<Chair> chairs;

    private Dictionary<Chair, Character> seatedCharacters;

    public void Awake()
    {
        seatedCharacters = new Dictionary<Chair, Character>();
    }

    public Transform SeatCharacter(Character character)
    {
        var chair = GetNextAvailableChair();
        seatedCharacters.Add(chair, character);
        chair.DisableObstacle();
        return chair.transform;
    }

    private Chair GetNextAvailableChair()
    {
        return chairs.Where(x => !seatedCharacters.Keys.Contains(x)).FirstOrDefault();
    }
}
