using UnityEngine;

public class BreakingBlock:MonoBehaviour
{
    public string playerTag = "Player";
    [SerializeField] public float speed;

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject, speed);
        }
    }
}
