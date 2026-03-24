using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (particles != null)
            {
                particles.Play();
            }
        }
    }
}