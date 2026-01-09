using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private Player player; // Reference to PlayerController





private void Awake()
{
  player = FindObjectOfType<Player>();

    // Check if DialogueManager exists before subscribing
    if (DialogueManager.GetInstance() != null)
    {
        DialogueManager.GetInstance().OnDialogueEnd += UnlockPlayerMovement;
    }
    else
    {
        Debug.LogError("DialogueManager is missing from the scene! Make sure it's added.");
    }
}

private void Update()
{
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                LockPlayerMovement(true); // Lock player movement when dialogue starts
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void LockPlayerMovement(bool isLocked)
    {
        if (player != null)
        {
            player.playerInput = !isLocked; // Use playerInput from Player class
            Player.dialogue = isLocked; // Set the static dialogue variable
        }
    }

    private void OnTriggerEnter(Collider collider)
{
    if (collider.gameObject.tag == "Player")
    {
        playerInRange = true;
    }
}

private void OnTriggerExit(Collider collider)
{
    if (collider.gameObject.tag == "Player")
    {
        playerInRange = false;
    }
}

    private void UnlockPlayerMovement()
    {
        LockPlayerMovement(false);
    }

    private void OnDestroy()
{
    if (DialogueManager.GetInstance() != null)
    {
        DialogueManager.GetInstance().OnDialogueEnd -= UnlockPlayerMovement;
    }
}

}