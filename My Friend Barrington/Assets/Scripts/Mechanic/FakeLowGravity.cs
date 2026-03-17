using UnityEngine;

public class FakeLowGravity : MonoBehaviour
{
    public float gravityScale = 0.3f; // 1 = normal, 0.3 = floaty

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 gravity = Physics.gravity;
        rb.AddForce(gravity * (gravityScale - 1f), ForceMode.Acceleration);
    }
}