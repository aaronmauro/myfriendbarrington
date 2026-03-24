using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineCamera zoomOutCam;
    [SerializeField] private CinemachineBrain brain;

    [SerializeField] private float lingerTime = 2f;
    [SerializeField] float easeTime = 5f;
    private float originalEaseTime;

    private bool colliding = false;
    private bool isRunning = false;

    private void Awake()
    {
        zoomOutCam.enabled = false;
        playerCam.enabled = true;
        originalEaseTime = brain.DefaultBlend.Time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRunning)
        {
            colliding = true;
            StartCoroutine(CameraSwitch());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            colliding = false;
            StartCoroutine(ReturnCamera());
        }
    }

    private IEnumerator CameraSwitch()
    {
        brain.DefaultBlend.Time = easeTime;

        isRunning = true;

        playerCam.enabled = false;
        zoomOutCam.enabled = true;

        yield return new WaitForSeconds(lingerTime);

        isRunning = false;
    }

    private IEnumerator ReturnCamera()
    {
        brain.DefaultBlend.Time = originalEaseTime;

        yield return new WaitForSeconds(lingerTime);

        if (!colliding)
        {
            zoomOutCam.enabled = false;
            playerCam.enabled = true;
        }
    }
}