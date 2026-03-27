using UnityEngine;

public class RiftRotate : MonoBehaviour
{
    [SerializeField] public float floatHeight = 0.5f;
    [SerializeField] public float floatSpeed = 1f;

    [SerializeField] public float rockAngle = 60f;
    [SerializeField] public float rockSpeed = 1f;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        // Floating
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, newY, 0);

        // Rock left/right
        float angle = Mathf.Sin(Time.time * rockSpeed) * rockAngle;
        transform.rotation = startRot * Quaternion.Euler(0, angle, 0);
    }
}