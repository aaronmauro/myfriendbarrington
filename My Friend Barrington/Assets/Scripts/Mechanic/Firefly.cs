using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField]
    public GameObject FireflyEmitter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FireflyEmitter.SetActive(true);
        }
    }
}