using UnityEngine;

public class TriggerObjectSwitcher : MonoBehaviour
{
    public string triggeringTag = "Player";

    [Header("Objects To Destroy")]
    public GameObject[] objectsToDestroy;

    [Header("Objects To Enable")]
    public GameObject[] objectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggeringTag))
            return;

        // Destroy objects
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
                Destroy(obj);
        }

        // Enable objects
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}
