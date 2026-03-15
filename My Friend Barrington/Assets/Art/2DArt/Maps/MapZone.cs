using UnityEngine;

public class MapZone : MonoBehaviour
{
    public GameObject mapIcon;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mapIcon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mapIcon.SetActive(false);
        }
    }
}