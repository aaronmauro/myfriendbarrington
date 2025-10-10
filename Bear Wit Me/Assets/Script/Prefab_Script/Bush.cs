using UnityEngine;

public class Bush : MonoBehaviour
{
    // Component
    [SerializeField]
    private Collider playerCollider;
    [SerializeField]
    private LayerMask excludeLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting Compoennt
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCollider.excludeLayers = excludeLayer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollider.includeLayers = excludeLayer;
        }
    }
}
