using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor = 0.2f; // 0 = stuck to world, 1 = moves with camera
    public Transform cam;

    private Vector3 lastCamPos;
    private bool isInTrigger = false;

    void Start()
    {
        if (!cam) cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    // Only sets flag for this layer
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Only trigger for player
            isInTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isInTrigger = false;
    }

    void LateUpdate()
    {
        if (!isInTrigger) return; // Only update position if in trigger

        Vector3 delta = cam.position - lastCamPos;
        transform.position += new Vector3(delta.x * parallaxFactor, 0f, 0f);
        lastCamPos = cam.position;
    }
}
