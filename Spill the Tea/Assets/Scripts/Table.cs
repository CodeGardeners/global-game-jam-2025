using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Table : MonoBehaviour
{
    [SerializeField]
    private List<Chair> chairs;

    private Dictionary<Character, Chair> seatedCharacters;

    [SerializeField]
    private List<Sprite> tableSprites;

    public enum TableType
    {
        blue = 0,
        green = 1,
        red = 2,
        yellow = 3
    }

    [SerializeField]
    private TableType tableType;


    public void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        seatedCharacters = new Dictionary<Character, Chair>();
        renderer.sprite = tableSprites[(int)tableType];
    }

    public void Start()
    {
        foreach (var chair in chairs)
        {
            chair.SetTableType(tableType);
        }
    }

    public bool SeatCharacter(Character character)
    {
        var chair = GetNextAvailableChair();
        seatedCharacters.Add(character, chair);
        chair.DisableObstacle();
        character.SetTarget(chair.transform.position, Character.CharacterState.ToTable, CheckMusic);
        return seatedCharacters.Count == chairs.Count;
    }

    public void UnseatCharacter(Character character)
    {
        seatedCharacters[character].EnableObstacle();
        seatedCharacters.Remove(character);
    }

    private void CheckMusic(Character character)
    {
        if (seatedCharacters.Count != chairs.Count)
        {
            return;
        }

        var title = seatedCharacters.Keys.First().Title;
        foreach (var seated in seatedCharacters.Keys)
        {
            if (seated.Title != title)
            {
                return;
            }
        }
        Debug.Log("Hurra");
    }

    private Chair GetNextAvailableChair()
    {
        return chairs.Where(x => !seatedCharacters.Values.Contains(x)).FirstOrDefault();
    }
}
