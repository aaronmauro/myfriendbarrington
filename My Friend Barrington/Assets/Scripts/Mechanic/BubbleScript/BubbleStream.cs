using FMODUnity;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    private BoxCollider bxCollider;

    [SerializeField]
    private float force;
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference bubbleStreamEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bxCollider = GetComponent<BoxCollider>();
    }

    // When the player is inside the bubble stream, apply a continuous upward force
    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        //Debug.Log(player);

        if (other.gameObject.isPlayer())
        {
            player.inBubbleStream(force);
            //RuntimeManager.PlayOneShotAttached(bubbleStreamEvent, gameObject);
        }
    }
}
