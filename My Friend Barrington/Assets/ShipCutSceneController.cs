using UnityEngine;
using UnityEngine.Playables;

public class ShipCutsceneController : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private Rigidbody shipRb;
    [SerializeField] private Collider shipSolidCollider; // the ledge collider (trigger OFF)

    [Header("Cutscene (pick one)")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorTriggerName = "ShipFall";
    [SerializeField] private PlayableDirector director;

    [Header("Options")]
    [SerializeField] private bool disableShipColliderDuringCutscene = true;
    [SerializeField] private bool triggerOnlyOnce = true;

    private bool played;

    public void PlayCutscene(GameObject player)
    {
        if (triggerOnlyOnce && played) return;
        played = true;

        // Stop ship physics so animation/timeline can drive transform
        if (shipRb != null)
        {
            shipRb.velocity = Vector3.zero;
            shipRb.angularVelocity = Vector3.zero;
            shipRb.isKinematic = true;
        }

        if (disableShipColliderDuringCutscene && shipSolidCollider != null)
            shipSolidCollider.enabled = false;

        // Optional: disable player movement here (depends on your controller)
        // player.GetComponent<PlayerMovement>()?.SetEnabled(false);

        // Play cutscene
        if (director != null)
        {
            director.Play();
        }
        else if (animator != null)
        {
            animator.SetTrigger(animatorTriggerName);
        }
    }

    // Call this at the end of the cutscene (animation event or Timeline signal)
    public void EndCutscene()
    {
        if (shipSolidCollider != null)
            shipSolidCollider.enabled = true;

        // If after cutscene you want physics back (usually you don't if ship "fell away")
        // shipRb.isKinematic = false;
        // shipRb.useGravity = true;
    }
}

