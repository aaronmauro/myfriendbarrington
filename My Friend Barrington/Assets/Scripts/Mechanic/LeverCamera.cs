using Unity.Cinemachine;
using UnityEngine;
using System.Collections;


public class LeverCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera LeverCam;

    [SerializeField]
    private Lever LeverCheck;

    [SerializeField]
    private bool IsCameraTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }
    private void Awake()
    {
        LeverCam.enabled = false;
        IsCameraTrigger = true;
    }


    // Update is called once per frame
    void Update()
    {
        //if (LeverCheck.isUsed == true)
        //{
        //    IsCameraTrigger = true;
        //}
        if (LeverCheck.isUsed && IsCameraTrigger)
        {
            StartCoroutine(LeverCamChange(3f));
        }
     
    }
    private IEnumerator LeverCamChange(float waitTime)
    {

        LeverCam.enabled = true;
        yield return new WaitForSeconds(waitTime);
        IsCameraTrigger = false;
        LeverCam.enabled = false;
    }


}
