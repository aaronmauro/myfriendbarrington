using Unity.Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera playerCam;
    [SerializeField]
    private CinemachineCamera[] Cam;

    [SerializeField]
    private float zoomOutMultipliers;
    private bool startGameZoomOut;
    public bool touchedTV;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCam.enabled = true;
        startGameZoomOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGameZoomOut)
        {
            zoomOutAtStart();
        }
        if (touchedTV)
        {
            playerCam.enabled = false;
        }
    }

    private void zoomOutAtStart()
    {
        if (playerCam.Lens.FieldOfView <= 60)
        {
            playerCam.Lens.FieldOfView += Time.deltaTime * zoomOutMultipliers;
        }
        else
        {
            startGameZoomOut = false;
        }
    }
}
