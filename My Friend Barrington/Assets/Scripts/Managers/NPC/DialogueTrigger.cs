using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField]
    string npcName;
    [SerializeField]
    Sprite npcImage;


    private bool playerInRange;
    private Player player; // Reference to PlayerController
    private bool hasSubscribed = false;

    private bool hasTalked = false;

    // For subscribing to the InputAction on the InputManager
    private bool inputSubscribed = false;

    [SerializeField] GameObject rift;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

    }

    private void Start()
    {
        // Try to subscribe in Start instead of Awake
        TrySubscribeToDialogueManager();
        TrySubscribeToInputManager();
    }

    private void TrySubscribeToDialogueManager()
    {
        if (!hasSubscribed && DialogueManager.GetInstance() != null)
        {
            DialogueManager.GetInstance().OnDialogueEnd += UnlockPlayerMovement;
            hasSubscribed = true;
        }
    }

    private void TrySubscribeToInputManager()
    {
        var im = InputManager.GetInstance();
        if (!inputSubscribed && im != null && im.interactAction != null && im.interactAction.action != null)
        {
            im.interactAction.action.performed += OnInteractAction;
            inputSubscribed = true;
        }
    }

    private void Update()
    {
        // Keep trying to subscribe if managers come up later
        if (DialogueManager.GetInstance() == null || !inputSubscribed)
        {
            if (!hasSubscribed)
                TrySubscribeToDialogueManager();

            if (!inputSubscribed)
                TrySubscribeToInputManager();

            // still continue, we don't rely on Update polling for input anymore
        }

        if (playerInRange && DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
        }
        else
        {       
            visualCue.SetActive(false);
        }
    }

    // Called when the InputActionReference on the InputManager is triggered
    private void OnInteractAction(InputAction.CallbackContext context)
    {
        // Only start dialogue on performed phase (should always be the case for .performed)
        if (!context.performed) return;

        if (!playerInRange) return;

        Debug.Log("Starting dialogue from trigger via InputAction!");
        StartDialogue();
    }

    private void StartDialogue() // dialogue can be started in two places now - DV
    {
        var dm = DialogueManager.GetInstance();
        if (dm == null || dm.dialogueIsPlaying) return;

        dm.UpdateNpc(npcName, npcImage);
        dm.EnterDialogueMode(inkJSON);
        LockPlayerMovement(true);
        hasTalked = true;
        if (rift != null) rift.SetActive(true);
    }

    private void LockPlayerMovement(bool isLocked)
    {
        if (player != null)
        {
            player.freezePlayer(isLocked); // use new freezePlayer function
            Player.dialogue = isLocked; // Set the static dialogue variable
            // Ensure jump state follows lock state (locked -> can't jump)
            player.canJump = !isLocked;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") 
        {
            playerInRange = true;
            var dm = DialogueManager.GetInstance();
            dm.animator = gameObject.GetComponent<Animator>();
            if (player != null)
            {
                // disable jump while in range
                player.canJump = false;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!hasTalked)
            {
                StartDialogue(); // you can't run from me - DV
            }
            playerInRange = false;
            var dm = DialogueManager.GetInstance();
            dm.animator = null;

            // If dialogue was just started above, StartDialogue will handle lock state.
            // Only re-enable jump here if there is no active dialogue.
            if (player != null)
            {
                if (DialogueManager.GetInstance() == null || !DialogueManager.GetInstance().dialogueIsPlaying)
                {
                    player.canJump = true;
                }
            }
        }
    }

    private void UnlockPlayerMovement()
    {
        LockPlayerMovement(false);
        // Ensure jump is re-enabled when dialogue ends
        if (player != null)
        {
            player.canJump = true;
        }
    }

    private void OnDestroy()
    {
        if (DialogueManager.GetInstance() != null)
        {
            DialogueManager.GetInstance().OnDialogueEnd -= UnlockPlayerMovement;
        }

        var im = InputManager.GetInstance();
        if (inputSubscribed && im != null && im.interactAction != null && im.interactAction.action != null)
        {
            im.interactAction.action.performed -= OnInteractAction;
            inputSubscribed = false;
        }
    }



}