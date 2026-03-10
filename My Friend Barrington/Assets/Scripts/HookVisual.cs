using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

public class HookVisual : MonoBehaviour
{
    [Header("Hook Indicator")]
    [SerializeField] private GameObject HookIndicator;
    [SerializeField] private bool autoSetTriggerRadius = true;

    private bool playerInRange;
    private GrapplePoint grapplePoint;
    private SphereCollider triggerCollider;

    void Awake()
    {
        // Try to find the GrapplePoint on the same object or a parent
        grapplePoint = GetComponent<GrapplePoint>() ?? GetComponentInParent<GrapplePoint>();

        // Try to find a SphereCollider to use as the trigger
        triggerCollider = GetComponent<SphereCollider>() ?? GetComponentInChildren<SphereCollider>();

        // If requested, set the collider radius from the GrapplePoint.activationRange
        if (autoSetTriggerRadius && grapplePoint != null && triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
            triggerCollider.radius = grapplePoint.activationRange;
        }
    }

    void Start()
    {
        if (HookIndicator != null)
            HookIndicator.SetActive(false);
    }

    void Update()
    {
        if (HookIndicator == null) return;

        HookIndicator.SetActive(playerInRange);
    }

    // Public accessor to get the GrapplePoint activationRange
    public float GetGrapplePointRange()
    {
        return grapplePoint != null ? grapplePoint.activationRange : 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
