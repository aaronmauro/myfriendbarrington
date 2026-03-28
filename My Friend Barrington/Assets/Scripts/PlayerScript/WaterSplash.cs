using UnityEngine;
using FMODUnity;

public class FMODTriggerSound : MonoBehaviour
{
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference waterSplashEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (ensure your player object is tagged "Player")
        if (other.CompareTag("Player"))
        {
            // Play the FMOD event as a one-shot at the position of the trigger object
            RuntimeManager.PlayOneShotAttached(waterSplashEvent, gameObject);
        }
    }
}