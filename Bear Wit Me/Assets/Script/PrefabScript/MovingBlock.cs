using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
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
        
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

       
        float distanceToCurrent = Vector3.Distance(transform.position, currentTarget.position);

       
        if (distanceToCurrent < switchDistance)
        {
            currentTarget = (currentTarget == targetA) ? targetB : targetA;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make player follow the moving platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Detach player when they leave the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
