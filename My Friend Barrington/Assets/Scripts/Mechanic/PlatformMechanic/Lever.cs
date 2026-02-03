using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] 
    private KeyCode interactButton = KeyCode.E;
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

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(startEnabled);

        // Initialize second target with the same startEnabled value (if assigned)
        if (secondTargetObject != null)
            secondTargetObject.SetActive(startEnabled);

        // Ensure the lever animation does not play until the player presses E.
        // Disabling the Animator prevents it from updating/playing any default clip.
        if (leverAnimator != null)
            leverAnimator.enabled = false;
    }

    void Update()
    {
        if (isTrigger && targetObject != null && Input.GetKeyDown(interactButton) && isUsed == false)
        {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isTrigger = false;
    }
}
