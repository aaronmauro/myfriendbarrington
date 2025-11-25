using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    private Transform currentTarget;
    public float speed = 0.5f;
    public float switchDistance = 0.05f;
    // sorry changing the script Eric
    // draw gizmos
    public Color color = Color.red;
    public Color color2 = Color.green;
    public float sphereSize = 1.0f;


    void Start()
    {
        if (currentTarget == null)
        {
            currentTarget = targetA;
        }
    }


    void FixedUpdate()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

       
        float distanceToCurrent = Vector3.Distance(transform.position, currentTarget.position);

       
        if (distanceToCurrent < switchDistance)
        {
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    }

    //  Handle player collision with the moving platform
    private void OnCollisionEnter(Collision collision)
    {
        // Make player follow the moving platform
        if (collision.gameObject.isPlayer())
        {
            collision.transform.SetParent(transform);
        }
    }

    // Detach player when they leave the platform
    private void OnCollisionExit(Collision collision)
    {
        // Detach player when they leave the platform
        if (collision.gameObject.isPlayer())
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(targetA.position, sphereSize);
        Gizmos.color = color2;
        Gizmos.DrawWireSphere(targetB.position, sphereSize);
    }
}
