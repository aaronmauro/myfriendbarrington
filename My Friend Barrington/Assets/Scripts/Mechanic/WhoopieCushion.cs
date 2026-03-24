using UnityEngine;
using System.Collections;

public class WhoopieCushion : MonoBehaviour
{
    Vector3 originalScale;
    public float squashAmount = 0.8f;
    public float speed = 8f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(Squash());
        }
    }

    IEnumerator Squash()
    {
        Vector3 squashed = new Vector3(originalScale.x, originalScale.y , originalScale.z * squashAmount);

        // squash down
        float t = 0;
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
    }
}