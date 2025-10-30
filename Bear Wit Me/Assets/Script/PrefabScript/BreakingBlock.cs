using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    public string playerTag = "Player";
    public float respawnDelay = 2f;
    public float destroyDelay = 0.75f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
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
