using System.Collections;
using UnityEngine;

public class Rift : MonoBehaviour
{
    [SerializeField]
    private GameObject tpTransform;
    private Rift otherRift;
    private Player player;

    [Header("Teleport Setting")]
    [SerializeField]
    private float tpCooldown;
    public bool isTp;
    public bool hideSpawn;

    private void Start()
    {
        // Get Player component
        GameObject findPlayer = GameObject.Find(GeneralGameTags.Player);
        otherRift = tpTransform.GetComponent<Rift>();
        player = findPlayer.GetComponent<Player>();
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
                player.transform.position = tpTransform.transform.position;
                isTp = true;
                otherRift.isTp = true;
                StartCoroutine(startCooldown());
                otherRift.StartCoroutine(otherRift.startCooldown());
            }
        }
    }

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
}
