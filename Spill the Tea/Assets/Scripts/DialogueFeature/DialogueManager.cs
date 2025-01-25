using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour, ICounterToDialogue
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    private static DialogueManager instance;
    public bool dialogueIsPlaying { get; private set; }
    private bool dialogueFinished;

    private void Awake()
    {
        // Check that only 1 instance is in the scene
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in Scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueFinished = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying) // Return if the dialogue is not playing
        {
            return;
        }

        // TODO Handle forwarding the story:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialoguemode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialogueFinished = true;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void ResetDialogue()
    {
        dialogueIsPlaying = false;
        dialogueFinished = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public bool GetDialogueFinished()
    {
        return dialogueFinished;
    }
}
