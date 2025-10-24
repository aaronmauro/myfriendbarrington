using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    public Transform currentTarget;
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

        // Check distances
        float distanceToCurrent = Vector3.Distance(transform.position, currentTarget.position);

        // If close enough, switch to the other target
        if (distanceToCurrent < switchDistance)
        {
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    }
}

