using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    // Tag assigned to the player object
    //public string playerTag = "Player";
    public float respawnDelay = 2f;
    public float destroyDelay = 0.75f;

    // Animator
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Renderer[] r;

    private void Start()
    {
        anim = GetComponent<Animator>();
        r = GetComponentsInChildren<Renderer>();
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the player tag
        if (collision.gameObject.isPlayer())
        {
            Invoke(nameof(Delay), destroyDelay);
            
            Invoke(nameof(Reactivate), respawnDelay);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the player tag
        if (other.gameObject.isPlayer())
        {
            anim.SetTrigger("Break");
            Invoke(nameof(Delay), destroyDelay);

            Invoke(nameof(Reactivate), respawnDelay);
        }
    }

    private void Reactivate()
    {
        //gameObject.SetActive(true); this will turn off code
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }
        foreach (Renderer ren in r)
        {
            ren.enabled = true;
        }
    }
    private void Delay()
    {
        //gameObject.SetActive(false);
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        foreach (Renderer ren in r)
        {
            ren.enabled = false;
        }
    }
}
