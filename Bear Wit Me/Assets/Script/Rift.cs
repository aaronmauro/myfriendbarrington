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

    private void Start()
    {
        GameObject findPlayer = GameObject.Find("Player");
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isTp)
            {
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
        yield return new WaitForSeconds(tpCooldown);
        isTp = false;
    }
}
