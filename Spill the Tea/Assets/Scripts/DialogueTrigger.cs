using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange; // Checks if player can talk with NPC

    private void Awake()
    {
        
    }

    private void Update()
    {
        // Check here if player can talk with Range or Order of NPCs
        if (Input.GetKeyDown(KeyCode.Space)) // Trigger to start the dialog
        {
            Debug.Log("Pressing Space");
            DialogueManager.GetInstance().EnterDialoguemode(inkJSON);
        }
    }
}
