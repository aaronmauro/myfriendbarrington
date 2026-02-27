using Unity.Cinemachine;
using UnityEngine;

public class SpecialMovingBlock : MonoBehaviour
{

    [SerializeField]
    private CinemachineCamera playerCam;
    [SerializeField]
    private float zoomOutMultipliers;
    [SerializeField]
    private int zoomOutValue;
    [SerializeField]
    private int zoomOutValueDefault;
    [SerializeField]
    private int zoomOutValueNew;

    bool zoomOut = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (zoomOut)
        {
            if (playerCam.Lens.FieldOfView <= zoomOutValueNew)
            {
                playerCam.Lens.FieldOfView += Time.deltaTime * zoomOutMultipliers;
            }
        } else if (playerCam.Lens.FieldOfView >= zoomOutValueDefault)
        {
            playerCam.Lens.FieldOfView -= Time.deltaTime * zoomOutMultipliers;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<NewMonoBehaviourScript>().speed = 4; // another vidberg classic - DV
        zoomOut = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        zoomOut = false;
    }
}
