using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Update()
    {
        // Check here if player can talk with Range or Order of NPCs
        if (Input.GetKeyDown(KeyCode.Space)) // Trigger to start the dialog
        {
            DialogueManager.GetInstance().EnterDialoguemode(inkJSON);
        }
    }
}
