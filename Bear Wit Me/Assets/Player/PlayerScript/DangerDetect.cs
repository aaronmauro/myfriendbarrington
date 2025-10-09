using UnityEngine;

public class DangerDetect : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        GameObject gManager = GameObject.Find("GameManager");
        gm = gManager.GetComponent<GameManager>();
    }
    // Checking if danger ahead
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            gm.dangerDetect = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            gm.dangerDetect = false;
        }
    }
}
