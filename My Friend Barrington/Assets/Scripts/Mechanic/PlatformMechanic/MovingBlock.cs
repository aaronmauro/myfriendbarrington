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
        Vector3 oldPos = transform.position;

        Vector3 move = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.fixedDeltaTime
        ) - transform.position;

        // Horizontal platforms: use transform.position directly to reduce jitter
        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            transform.position += new Vector3(move.x, 0f, 0f);
        }
        else // Vertical platforms: keep Rigidbody.MovePosition for proper physics
        {
            rb.MovePosition(transform.position + move);
        }

        PlatformDelta = transform.position - oldPos;

        if (Vector3.Distance(transform.position, currentTarget.position) < switchDistance)
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

