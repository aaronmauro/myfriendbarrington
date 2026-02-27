using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    // New: second serialized target object
    [SerializeField]
    private GameObject secondTargetObject;

    [SerializeField]
    private bool startEnabled = false;

    [SerializeField]
    public bool isUsed;

    [SerializeField]
    private ParticleSystem interactParticles;

    // New: assign the Animator that controls the lever animation
    [SerializeField]
    private Animator leverAnimator;

    // Name of the trigger parameter in the Animator
    private const string AnimatorTriggerName = "Activate";

    private bool isTrigger;

    // Input subscription tracking
    private bool inputSubscribed = false;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(startEnabled);

        // Initialize second target with the same startEnabled value (if assigned)
        if (secondTargetObject != null)
            secondTargetObject.SetActive(startEnabled);

        // Ensure the lever animation does not play until the player presses the interact action.
        if (leverAnimator != null)
            leverAnimator.enabled = false;

        TrySubscribeToInputManager();
    }

    void Update()
    {
        // Try to subscribe in case InputManager is created after this object
        if (!inputSubscribed)
            TrySubscribeToInputManager();
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

    private void OnInteractAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!isTrigger) return;
        if (targetObject == null) return;
        if (isUsed) return;

        targetObject.SetActive(!targetObject.activeSelf);

        // Toggle second target if assigned
        if (secondTargetObject != null)
            secondTargetObject.SetActive(!secondTargetObject.activeSelf);

        isUsed = true;

        if (interactParticles != null)
            interactParticles.Play();

        // Enable the Animator (so it can run) and trigger the animation.
        if (leverAnimator != null)
        {
            leverAnimator.enabled = true;
            leverAnimator.SetTrigger(AnimatorTriggerName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            other.GetComponent<Player>().isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            other.GetComponent<Player>().isInteracting = false;
        }
    }

    private void OnDestroy()
    {
        if (!inputSubscribed) return;

        var im = InputManager.GetInstance();
        if (im != null && im.interactAction != null && im.interactAction.action != null)
        {
            im.interactAction.action.performed -= OnInteractAction;
            inputSubscribed = false;
        }
    }
}