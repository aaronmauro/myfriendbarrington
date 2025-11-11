using UnityEngine;

public class PillowFort : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    private Transform currentTarget;
    public float speed = 0.5f;
    public float switchDistance = 0.05f;

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

       // Calculate distance to current target
        float distanceToCurrent = Vector3.Distance(transform.position, currentTarget.position);

       // Switch target if close enough
        if (distanceToCurrent < switchDistance)
        {
            // Switch target
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    }

}