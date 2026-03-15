using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private CinemachineCamera zoomOutCam;

    [SerializeField] private float lingerTime = 2f;

    private bool colliding = false;
    private bool isRunning = false;

    private void Awake()
    {
        zoomOutCam.enabled = false;
        playerCam.enabled = true;
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
        isRunning = true;

        playerCam.enabled = false;
        zoomOutCam.enabled = true;

        yield return new WaitForSeconds(lingerTime);

        isRunning = false;
    }

    private IEnumerator ReturnCamera()
    {
        yield return new WaitForSeconds(.7f);

        if (!colliding)
        {
            zoomOutCam.enabled = false;
            playerCam.enabled = true;
        }
    }
}