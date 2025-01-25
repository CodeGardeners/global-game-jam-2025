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

    private Dictionary<Character, Chair> seatedCharacters;

    public void Awake()
    {
        renderer = GetComponent<Renderer>();
        seatedCharacters = new Dictionary<Character, Chair>();
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

    public void SetColor(Color color)
    {
        renderer.material.color = color;
        foreach (var chair in chairs)
        {
            chair.SetColor(color);
        }
    }

    private void CheckMusic()
    {
        if (seatedCharacters.Count != chairs.Count)
        {
            return;
        }

        var title = seatedCharacters.Keys.First().Title;
        foreach (var character in seatedCharacters.Keys)
        {
            if (character.Title != title)
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
