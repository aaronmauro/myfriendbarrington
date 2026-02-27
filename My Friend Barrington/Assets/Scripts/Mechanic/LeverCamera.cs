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

    [SerializeField] 
    private float lingerTime = 3f;  

    private bool isRunning;          

    void Start()
    {
     
    }

    private void Awake()
    {
        LeverCam.enabled = false;
        IsCameraTrigger = true;
    }

    void Update()
    {
        if (LeverCheck.isUsed && IsCameraTrigger && !isRunning) 
        {
            StartCoroutine(LeverCamChange(lingerTime)); 
        }
    }

    private IEnumerator LeverCamChange(float waitTime)
    {
        isRunning = true; 

        LeverCam.enabled = true;
        yield return new WaitForSeconds(waitTime);
        IsCameraTrigger = false;
        LeverCam.enabled = false;

        isRunning = false;  
    }
}
