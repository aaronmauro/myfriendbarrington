using UnityEngine;

public class Bush : MonoBehaviour
{
    // Component
    [SerializeField]
    private Collider playerCollider;
    [SerializeField]
    private LayerMask excludeLayer;
    [SerializeField]
    private SoundWaves waves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            waves.inBush = true;
        }
    }
    /*
    // When Player is inside the bush collider
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("help");
            
        }
    }
    */

    //  When Player exits the bush collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            waves.inBush = false;
        }
    }
}
