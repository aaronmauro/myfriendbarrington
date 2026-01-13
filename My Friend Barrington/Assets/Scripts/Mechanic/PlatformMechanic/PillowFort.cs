using Unity.VisualScripting;
using UnityEngine;

public class PillowFort : MonoBehaviour
{
 
    public Transform target;
    public Transform Object;
    public float speed;

    void Update()
    {
     
        Object.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
    
        if (Object.position == target.position)
        {
          
            Debug.Log("Target reached!");
            enabled = false;
        }
    }
}


