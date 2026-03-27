using FMODUnity;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    private BoxCollider bxCollider;

    [SerializeField]
    private float force;
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference bubbleStreamEvent;
    [SerializeField] private ParticleSystem bubbleSystem;
    [SerializeField] private bool bubbleActive = false;
    private bool inBubble;
    private float audioTimer = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bxCollider = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        if (!inBubble) return;

        audioTimer += Time.deltaTime;
        if (audioTimer > 3.7f)
        {
            RuntimeManager.PlayOneShotAttached(bubbleStreamEvent, gameObject);
            audioTimer = 0f;
        }
    }

    // When the player is inside the bubble stream, apply a continuous upward force
    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        //Debug.Log(player);
        //Debug.Log("something");
        if (other.gameObject.isPlayer() && bubbleActive)
        {
           
            player.inBubbleStream(force);
            RuntimeManager.PlayOneShotAttached(bubbleStreamEvent, gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GeneralGameTags.Box) && bubbleSystem != null)
        {
            bubbleSystem.Play();
            bubbleActive = true;
        }
        else if (other.gameObject.isPlayer())
        {
            inBubble = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            inBubble = false;
            audioTimer = 5.0f;
        }
    }
}
