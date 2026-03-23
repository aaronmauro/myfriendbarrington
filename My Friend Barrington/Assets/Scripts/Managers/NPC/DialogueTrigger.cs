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

    [SerializeField] string npcName;
    [SerializeField] Sprite npcImage;

    private bool playerInRange;
    private Player player;
    private bool hasSubscribed = false;
    private bool hasTalked = false;

    private bool inputSubscribed = false;

    // ✅ MULTIPLE RIFTS
    [SerializeField] private List<GameObject> rifts = new List<GameObject>();

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
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
        if (DialogueManager.GetInstance() == null || !inputSubscribed)
        {
            if (!hasSubscribed)
                TrySubscribeToDialogueManager();

            if (!inputSubscribed)
                TrySubscribeToInputManager();
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

    private void OnInteractAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!playerInRange) return;

        Debug.Log("Starting dialogue from trigger via InputAction!");
        StartDialogue();
    }

    private void StartDialogue()
    {
        var dm = DialogueManager.GetInstance();
        if (dm == null || dm.dialogueIsPlaying) return;

        dm.UpdateNpc(npcName, npcImage);
        dm.EnterDialogueMode(inkJSON);
        LockPlayerMovement(true);
        hasTalked = true;

        //  ACTIVATE ALL RIFTS
        foreach (GameObject rift in rifts)
        {
            if (rift != null)
            {
                rift.SetActive(true);
            }
        }
    }

    private void LockPlayerMovement(bool isLocked)
    {
        if (player != null)
        {
            player.freezePlayer(isLocked);
            Player.dialogue = isLocked;
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
                StartDialogue();
            }

            playerInRange = false;
            var dm = DialogueManager.GetInstance();
            dm.animator = null;

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