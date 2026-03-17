using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Rift : MonoBehaviour
{
    // getting componenet
    [SerializeField]
    private GameObject tpTransform;
    private Rift otherRift;
    private Player player;
    private CinemachineFollow playerCam;

    [Header("Teleport Setting")]
    [SerializeField]
    private float tpCooldown;
    public bool isTp;
    private bool isTpCam;
    public bool hideSpawn;
    [SerializeField]
    private float triggerDistance;

    private void Start()
    {
        // Get Player component
        otherRift = tpTransform.GetComponent<Rift>();
        player = gameObject.findPlayer();
        playerCam = GameObject.Find(GeneralGameTags.PlayerCamera).GetComponent<CinemachineFollow>();

        isTp = false;
        //Debug.Log(playerCam);
    }

    private void FixedUpdate()
    {
        //Debug.Log(isTpCam);
        if (isTpCam)
        {
            StartCoroutine(startFollowing());
        }
        
        //startPlayerCameraFollowing();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Teleport Player
        if (other.gameObject.isPlayer())
        {
            if (!isTp)
            {
                // Teleport the player to the other rift's position
                playerCam.TrackerSettings.PositionDamping = Vector3.zero;
                player.transform.position = tpTransform.transform.position;
                Debug.Log("teleporting");
                isTpCam = true;
                Debug.Log("hello world");
                isTp = true;
                otherRift.isTp = true;
                StartCoroutine(startCooldown());
                otherRift.StartCoroutine(otherRift.startCooldown());
            }
        }
    }

    // cooldown for tp again
    private IEnumerator startCooldown()
    {
        // Wait for cooldown duration
        yield return new WaitForSeconds(tpCooldown);
        isTp = false;
        if (hideSpawn)
        {
            gameObject.SetActive(false);
        }
    }

    // cooldown for camera
    private IEnumerator startFollowing()
    {
        isTpCam = false;
        yield return new WaitForSeconds(0.1f);
        playerCam.TrackerSettings.PositionDamping = Vector3.one;
        Debug.Log("start following");   
    }

    private void startPlayerCameraFollowing()
    {
        playerCam.TrackerSettings.PositionDamping = Vector3.one;
        Debug.Log("start following");
        isTpCam = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
