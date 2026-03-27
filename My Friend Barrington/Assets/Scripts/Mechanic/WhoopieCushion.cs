using UnityEngine;
using System.Collections;

public class WhoopieCushion : MonoBehaviour
{
    Vector3 originalScale;

    public float squashAmount = 0.8f;
    public float speed = 8f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem landingParticles;

    private bool isSquashing = false; // ✅ added flag

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isSquashing)
        {
            StartCoroutine(Squash());

            if (landingParticles != null)
            {
                landingParticles.Play();
            }
        }
    }

    IEnumerator Squash()
    {
        isSquashing = true; // ✅ prevent re-triggering

        Vector3 squashed = new Vector3(
            originalScale.x,
            originalScale.y,
            originalScale.z * squashAmount
        );

        float t = 0;

        // squash down
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(originalScale, squashed, t);
            yield return null;
        }

        // bounce back up
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(squashed, originalScale, t);
            yield return null;
        }

        isSquashing = false; // ✅ allow future triggers
    }
}