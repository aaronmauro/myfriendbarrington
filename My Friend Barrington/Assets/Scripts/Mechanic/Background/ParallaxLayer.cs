using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor = 0.2f;   // 0 = stuck to world, 1 = moves with camera (no parallax)
    public float parallaxFactorY = 0f;
    public Transform cam;

    private Vector3 lastCamPos;

    void Start()
    {
        if (!cam) cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;

        // Usually parallax only on X for platformers; enable Y if you want vertical parallax too.
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactorY, 0f);

        lastCamPos = cam.position;
    }
}