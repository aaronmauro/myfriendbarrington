using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (particles != null)
            {
                particles.Play();
            }
        }
    }
}