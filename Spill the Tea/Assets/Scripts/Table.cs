using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Table : MonoBehaviour
{
    [SerializeField]
    private List<Chair> chairs;

    private new Renderer renderer;

    private Dictionary<Chair, Character> seatedCharacters;

    public void Awake()
    {
        renderer = GetComponent<Renderer>();
        seatedCharacters = new Dictionary<Chair, Character>();
    }

    public Transform SeatCharacter(Character character)
    {
        var chair = GetNextAvailableChair();
        seatedCharacters.Add(chair, character);
        chair.DisableObstacle();
        return chair.transform;
    }

    public void SetColor(Color color)
    {
        renderer.material.color = color;
        foreach (var chair in chairs)
        {
            chair.SetColor(color);
        }
    }

    private Chair GetNextAvailableChair()
    {
        return chairs.Where(x => !seatedCharacters.Keys.Contains(x)).FirstOrDefault();
    }
}
