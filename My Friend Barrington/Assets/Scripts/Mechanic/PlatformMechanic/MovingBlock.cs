using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;

    private Transform currentTarget;
    private Rigidbody rb;

    public float speed = 0.5f;
    public float switchDistance = 0.05f;

    public Vector3 PlatformDelta { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        currentTarget = targetA;
    }

    void FixedUpdate()
    {
        Vector3 oldPos = rb.position;

        Vector3 newPos = Vector3.MoveTowards(
            rb.position,
            currentTarget.position,
            speed * Time.fixedDeltaTime
        );

        PlatformDelta = newPos - oldPos;
        rb.MovePosition(newPos);

        if (Vector3.Distance(newPos, currentTarget.position) < switchDistance)
        {
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    }

    private void OnDrawGizmos()
    {
        if (targetA != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetA.position, 1f);
        }

        if (targetB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(targetB.position, 1f);
        }
    }
}