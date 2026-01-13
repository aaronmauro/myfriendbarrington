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
    private bool hasSubscribed = false;





    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        // Try to subscribe in Start instead of Awake
        TrySubscribeToDialogueManager();
    }

    private void TrySubscribeToDialogueManager()
    {
        if (!hasSubscribed && DialogueManager.GetInstance() != null)
        {
            DialogueManager.GetInstance().OnDialogueEnd += UnlockPlayerMovement;
            hasSubscribed = true;
        }
    }


    private void Update()
    {
        if (DialogueManager.GetInstance() == null)
        {
            if (!hasSubscribed)
            {
                TrySubscribeToDialogueManager();
            }
            return;
        }

        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            // Try direct input check
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Starting dialogue from trigger!");
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                LockPlayerMovement(true);
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