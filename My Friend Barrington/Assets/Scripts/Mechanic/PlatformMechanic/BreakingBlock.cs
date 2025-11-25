using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    // Tag assigned to the player object
    //public string playerTag = "Player";
    public float respawnDelay = 2f;
    public float destroyDelay = 0.75f;
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the player tag
        if (collision.gameObject.isPlayer())
        {
            Invoke(nameof(Delay), destroyDelay);
            
            Invoke(nameof(Reactivate), respawnDelay);
        }
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
    private void Delay()
    {
        gameObject.SetActive(false);
    }
}
