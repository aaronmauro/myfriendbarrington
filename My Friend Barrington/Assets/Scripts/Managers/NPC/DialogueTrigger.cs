using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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

    // For subscribing to the InputAction on the InputManager
    private bool inputSubscribed = false;

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

        var dm = DialogueManager.GetInstance();
        if (dm == null || dm.dialogueIsPlaying) return;

        Debug.Log("Starting dialogue from trigger via InputAction!");
        dm.UpdateNpc(npcName, npcImage);
        dm.EnterDialogueMode(inkJSON);
        LockPlayerMovement(true);
    }

    private void LockPlayerMovement(bool isLocked)
    {
        if (player != null)
        {
            player.freezePlayer(isLocked); // use new freezePlayer function
            Player.dialogue = isLocked; // Set the static dialogue variable
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") 
        {
            playerInRange = true;
            var dm = DialogueManager.GetInstance();
            dm.animator = gameObject.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            var dm = DialogueManager.GetInstance();
            dm.animator = null;
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

        var im = InputManager.GetInstance();
        if (inputSubscribed && im != null && im.interactAction != null && im.interactAction.action != null)
        {
            im.interactAction.action.performed -= OnInteractAction;
            inputSubscribed = false;
        }
    }



}