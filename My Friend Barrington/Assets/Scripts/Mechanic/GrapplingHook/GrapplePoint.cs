using UnityEngine;


//A point in the world that the player can grapple to.
public class GrapplePoint : MonoBehaviour
{
    public float activationRange = 20f;

    public bool IsInRange(Vector3 playerPosition)
    {
        return Vector3.Distance(transform.position, playerPosition) <= activationRange;
    }
}