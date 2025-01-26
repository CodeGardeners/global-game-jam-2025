using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDialog
{
    public int characterId = -1; 
    public TextAsset inkFile; // Reference to the Ink file for dialog
}

[CreateAssetMenu(fileName = "DialogAssociator", menuName = "ScriptableObjects/DialogAssociator", order = 1)]
public class DialogAssociator : ScriptableObject
{
    public List<CharacterDialog> characterDialogs = new List<CharacterDialog>();
    // Use a dictionary for faster lookups
    private Dictionary<int, TextAsset> characterDialogMap = new Dictionary<int, TextAsset>();

    /// <summary>
    /// Initialize the dictionary on first use.
    /// </summary>
    private void InitializeDictionary()
    {
        // Clear the dictionary to avoid duplicates
        characterDialogMap.Clear();

        foreach (var entry in characterDialogs)
        {
            if (entry.characterId != -1 && !characterDialogMap.ContainsKey(entry.characterId))
            {
                characterDialogMap[entry.characterId] = entry.inkFile;
            }
        }
    }
    /// <summary>
    /// Get the corresponding Ink file for a given character ID.
    /// </summary>
    /// <param name="characterId">The ID of the character</param>
    /// <returns>The associated Ink file (TextAsset) or null if not found</returns>
    public TextAsset GetInkFileForCharacter(int characterId)
    {
        // Initialize dictionary if not already initialized
        if (characterDialogMap.Count == 0)
        {
            InitializeDictionary();
        }

        // Attempt to fetch the Ink file
        if (characterDialogMap.TryGetValue(characterId, out var inkFile))
        {
            return inkFile;
        }

        // Log a warning if the ID is invalid
        Debug.LogWarning($"No dialog found for character ID: {characterId}");
        return null;
    }
}