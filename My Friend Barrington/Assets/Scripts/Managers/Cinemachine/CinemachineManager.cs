using Unity.Cinemachine;
using Unity.VisualScripting;
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
    [SerializeField]
    private int zoomOutValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerCam != null)
        {
            playerCam.enabled = true;
            startGameZoomOut = true;
        }
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
        if (playerCam.Lens.FieldOfView <= zoomOutValue)
        {
            playerCam.Lens.FieldOfView += Time.deltaTime * zoomOutMultipliers;
        }
        else
        {
            startGameZoomOut = false;
        }
    }
}
