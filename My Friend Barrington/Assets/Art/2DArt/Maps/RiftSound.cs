using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class RiftSound : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature riftShader;
    [SerializeField] private Material CRT;
    public AudioSource audioSource;
    [Header("Fade Settings")]
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private string intensityProperty = "_Intensity";

    private Coroutine _fadeCoroutine;

    private void Start()
    {
        CRT = new Material(CRT);
        CRT.SetFloat(intensityProperty, 0f);
        riftShader.passMaterial = CRT;
        audioSource.volume = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        riftShader.passMaterial = CRT;
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeIn());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        audioSource.volume = 0f;
        audioSource.Play();
        yield return StartCoroutine(Fade(0f, 1f, fadeInDuration, removeOnComplete: false));
    }

    private IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(1f, 0f, fadeOutDuration, removeOnComplete: true));
        audioSource.Stop();
        audioSource.volume = 0f; // reset for next entry
    }

    private IEnumerator Fade(float from, float to, float duration, bool removeOnComplete)
    {
        float elapsed = 0f;
        float startVolume = audioSource.volume;
        CRT.SetFloat(intensityProperty, from);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            CRT.SetFloat(intensityProperty, Mathf.Lerp(from, to, t));
            audioSource.volume = Mathf.Lerp(startVolume, to, t);
            yield return null;
        }

        CRT.SetFloat(intensityProperty, to);
        audioSource.volume = to;
        if (removeOnComplete)
            riftShader.passMaterial = null;
    }
}