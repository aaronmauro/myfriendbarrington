using UnityEngine;

public class BreakingBlock:MonoBehaviour
{
    public string playerTag = "Player";
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject, 2f);
        }
    }
}
