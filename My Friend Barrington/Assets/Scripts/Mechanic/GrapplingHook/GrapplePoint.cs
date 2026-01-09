using UnityEngine;


//A point in the world that the player can grapple to.
public class GrapplePoint : MonoBehaviour
{
    public float activationRange = 20f;

    // Draw Gizmos
    private Color gizColor = Color.yellow;

    public bool IsInRange(Vector3 playerPosition)
    {
        return Vector3.Distance(transform.position, playerPosition) <= activationRange;
    }
    // Draw Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = gizColor;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}