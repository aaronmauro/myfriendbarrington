using Unity.VisualScripting;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor = 0.2f;   // 0 = stuck to world, 1 = moves with camera (no parallax)
    public bool inArea = false;
    public Transform cam;

    private Vector3 lastCamPos;

    void Start()
    {
        if (!cam) cam = Camera.main.transform;
        lastCamPos = cam.position;
    }
    void OnTriggerEnter()
    
    {
        inArea = true;

}

    void OnTriggerExit()

    {
        inArea = false;

    }
    public void UpdatePOS()
    {
        Vector3 delta = cam.position - lastCamPos;

        // Usually parallax only on X for platformers; enable Y if you want vertical parallax too.
        transform.position += new Vector3(delta.x * parallaxFactor, 0f, 0f);

        lastCamPos = cam.position;
    }
    void LateUpdate()
    {
        if (inArea == true)
        {
            UpdatePOS();
        }
    }
}