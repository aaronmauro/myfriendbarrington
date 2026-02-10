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
    public bool hideSpawn;

    private void Start()
    {
        // Get Player component
        //GameObject findPlayer = GameObject.Find(GeneralGameTags.Player);
        otherRift = tpTransform.GetComponent<Rift>();
        player = gameObject.findPlayer();
        playerCam = GameObject.Find(GeneralGameTags.PlayerCamera).GetComponent<CinemachineFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        Debug.Log("apple");
        if (player != null)
        {
            Debug.Log("found");
        }
        else if (player == null)
        {
            Debug.Log("sad");
        }
        */
        // Teleport Player
        if (other.gameObject.isPlayer())
        {
            if (!isTp)
            {
                // Teleport the player to the other rift's position
                playerCam.TrackerSettings.PositionDamping = Vector3.zero;
                player.transform.position = tpTransform.transform.position;
                isTp = true;
                otherRift.isTp = true;
                StartCoroutine(startCooldown());
                StartCoroutine(startFollowing());
                otherRift.StartCoroutine(otherRift.startCooldown());
            }
        }
    }

    // cooldown for tp again
    private IEnumerator startCooldown()
    {
        // Wait for cooldown duration
        if (hideSpawn)
        {
            gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(tpCooldown);
        isTp = false;
    }

    // cooldown for camera
    private IEnumerator startFollowing()
    {
        yield return new WaitForSeconds(0.5f);
        playerCam.TrackerSettings.PositionDamping = Vector3.one;
    }
}
