using UnityEditor;
using UnityEngine;

public class DangerDetect : MonoBehaviour
{
    // ground check in front
    [SerializeField]
    private LayerMask floorMask;
    public Color gizmoColour = Color.yellow;
    public float rayLength;
    private Vector3 rayPosition;
    private bool isGround;
    public float dectectDistance;
    public bool direction;
    // getting game manager
    private GameManager gm;
    
    // start
    private void Start()
    {
        gm = gameObject.findGameManager();

        direction = true;
    }

    private void Update()
    {
        // Run methods
        dynamicRespawn();
    }
    // Checking if danger ahead
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GeneralGameTags.DangerBox))
        {
            gm.dangerDetect = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GeneralGameTags.DangerBox))
        {
            gm.dangerDetect = false;
        }
    }

    // dynamic Respawn methods
    private void dynamicRespawn()
    {
        // checking infront are floor
        if (direction)
        {
            rayPosition = new Vector3(transform.position.x + dectectDistance, transform.position.y, transform.position.z);
        }
        else if (!direction)
        {
            rayPosition = new Vector3(transform.position.x + -dectectDistance, transform.position.y, transform.position.z);
        }
        isGround = Physics.Raycast(rayPosition, Vector3.down, out RaycastHit hit, rayLength, floorMask);
        //Debug.Log(isGround);
        // checking ground
        if (!isGround)
        {
            gm.dangerDetect = true;
        }
        else
        {
            gm.dangerDetect = false;
        }
    }

    // draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawRay(rayPosition, Vector3.down * rayLength);
    }
}
