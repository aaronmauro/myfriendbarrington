using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;

    private Transform currentTarget;
    private Vector3 lastPosition;

    public float speed = 0.5f;
    public float switchDistance = 0.05f;

    public Vector3 PlatformDelta { get; private set; }

    void Start()
    {
        currentTarget = targetA;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        
        Vector3 oldPos = transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.fixedDeltaTime
        );

        PlatformDelta = transform.position - oldPos;

        if (Vector3.Distance(transform.position, currentTarget.position) < switchDistance)
        {
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    
    }

}