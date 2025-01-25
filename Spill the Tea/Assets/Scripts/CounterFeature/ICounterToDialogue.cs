using UnityEngine;

public interface ICounterToDialogue
{
    public void EnterDialoguemode(TextAsset inkJSON);
    public void ResetDialogue();
    public bool GetDialogueFinished();
}
