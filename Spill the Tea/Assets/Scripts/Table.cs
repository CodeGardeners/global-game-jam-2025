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
    }

    private void CheckMusicMatches(Character character)
    {
        var others = seatedCharacters.Keys.Where(x => x != character);
        AudioGuestCharacter newAudioGuestCharacter = character.transform.GetChild(1).GetComponent<AudioGuestCharacter>();
        var newDirector = newAudioGuestCharacter.playableDirector;
        foreach (var other in others)
        {
            AudioGuestCharacter audioGuestCharacter = other.transform.GetChild(1).GetComponent<AudioGuestCharacter>();
            if (audioGuestCharacter.playableDirector != newDirector)
            {
                PlayDistorted(true);
                return;
            }
        }
        if (seatedCharacters.Count == chairs.Count)
        {
            PlayDistorted(false);
            Debug.Log("All characters are playing the same piece");
            Lock();
        }
    }
    private void PlayDistorted(bool distorted){
        foreach (var character in seatedCharacters.Keys)
        {
            character.transform.GetChild(1).GetComponent<AudioGuestCharacter>().SetDistortedPlaying(distorted);
        }
    }

    private Chair GetNextAvailableChair()
    {
        return chairs.Where(x => !seatedCharacters.Values.Contains(x)).FirstOrDefault();
    }

    private void Lock(){
        Debug.Log("Locking table " + tableType);
        foreach (Character character in seatedCharacters.Keys)
        {
            character.Lock();
        }
        GameManager.Instance.addLockedTable();
    }
}
