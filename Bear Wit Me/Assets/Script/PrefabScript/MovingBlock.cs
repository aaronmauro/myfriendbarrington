using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    public Transform currentTarget;
    public float speed = 0.5f;

    void FixedUpdate()
    {

      float distanceToA = Vector3.Distance(transform.position, targetA.position);
      float distanceToB = Vector3.Distance(transform.position, targetB.position);

       if (distanceToA == 0f)
        {
            currentTarget = targetB;
        }

        if (distanceToB == 0f)
        {
            currentTarget = targetA;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }
    


}

