using UnityEngine;

public class SlowPlatform : MonoBehaviour
{
    [Header("Jump Reduction")]
    [Range(0.1f, 1f)]
    public float jumpMultiplier = 0.25f;

    private Rigidbody playerRb;
    private bool playerOnPlatform;
    private float originalLinearVelocityY;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerRb = collision.gameObject.GetComponent<Rigidbody>();
        if (playerRb == null) return;

        playerOnPlatform = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!playerOnPlatform || playerRb == null) return;

        Vector3 vel = playerRb.linearVelocity;

        // Only reduce upward velocity (jump)
        if (vel.y > 0f)
        {
            vel.y *= jumpMultiplier;
            playerRb.linearVelocity = vel;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerOnPlatform = false;
        playerRb = null;
    }
}
