using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RiftSound : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature riftShader;
    [SerializeField] private Material CRT;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            riftShader.passMaterial = CRT;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
            riftShader.passMaterial = null;
        }
    }
}