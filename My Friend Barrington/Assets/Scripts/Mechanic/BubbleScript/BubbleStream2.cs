using UnityEngine;
using FMODUnity;

public class BubbleStream2point2 : MonoBehaviour
{
    private BoxCollider bxCollider;

    [SerializeField] private float force;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference bubbleStreamEvent;
    [SerializeField] private ParticleSystem bubbleSystem;

    void Start()
    {
        bxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.inBubbleStream(force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GeneralGameTags.Box) && bubbleSystem != null)
        {
            bubbleSystem.Play();
        }
    }
}