using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    /* comment out until fixed, uncomment this to continue fixing - Eric
    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }

        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    */
    
    


    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.isPlayer()) // a bit tweaking the code
        {
            playerInRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            playerInRange = false;
        }
    }

}
