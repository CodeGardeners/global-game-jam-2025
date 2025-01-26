using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Audio;
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
        character.SetTarget(chair.transform.position, Character.CharacterState.ToTable, CheckMusicMatches);
        return seatedCharacters.Count == chairs.Count;
    }

    public void UnseatCharacter(Character character)
    {
        seatedCharacters[character].EnableObstacle();
        seatedCharacters.Remove(character);
        character.GetAudioGuestCharacter().SetDistortedPlaying(false);
        CheckMusicMatches();
    }

    private void CheckMusicMatches(Character character)
    {
        CheckMusicMatches();
    }

    private void CheckMusicMatches()
    {
        if(seatedCharacters.Count == 0){
            return;
        }
        var characters = seatedCharacters.Keys;
        var referenceDirector = characters.First().GetAudioGuestCharacter().playableDirector;
        bool matches = characters.All(x => x.GetAudioGuestCharacter().playableDirector == referenceDirector);
        PlayDistorted(!matches);
        if (matches && seatedCharacters.Count == chairs.Count)
        {
            Lock();
        }
    }

    private void PlayDistorted(bool distorted)
    {
        foreach (var character in seatedCharacters.Keys)
        {
            character.GetAudioGuestCharacter().SetDistortedPlaying(distorted);
        }
    }

    private Chair GetNextAvailableChair()
    {
        return chairs.Where(x => !seatedCharacters.Values.Contains(x)).FirstOrDefault();
    }

    private void Lock()
    {
        Debug.Log("Locking table " + tableType);
        foreach (Character character in seatedCharacters.Keys)
        {
            character.Lock();
        }
        GameManager.Instance.addLockedTable();
    }
}
