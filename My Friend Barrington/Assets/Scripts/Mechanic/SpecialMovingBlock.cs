using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

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

    private bool colliding = false;

    [SerializeField] bool DoTheCameraThing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!DoTheCameraThing) return;
        if (zoomOut)
        {
            if (playerCam.Lens.FieldOfView <= zoomOutValueNew)
            {
                playerCam.Lens.FieldOfView += Time.deltaTime * zoomOutMultipliers;
            }
        } else if (playerCam.Lens.FieldOfView >= zoomOutValueDefault) //This will lock the player camera - EH, TDLL
        {
            playerCam.Lens.FieldOfView -= Time.deltaTime * zoomOutMultipliers;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
        this.GetComponent<NewMonoBehaviourScript>().speed = 4; // another vidberg classic - DV
        zoomOut = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(2f);
        if (!colliding) zoomOut = false;
    }
}
