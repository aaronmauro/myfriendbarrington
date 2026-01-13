using Unity.Cinemachine;
using UnityEngine;

public class ZoomInAndOut : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera playerCam;
    private bool isExist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isExist = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCam.Lens.FieldOfView != 60 && isExist)
        {
            playerCam.Lens.FieldOfView -= Time.deltaTime * 10f;
        }
        else if (playerCam.Lens.FieldOfView >= 60)
        {
            isExist = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            playerCam.Lens.FieldOfView += Time.deltaTime * 10f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            isExist = true;
        }
    }
}
