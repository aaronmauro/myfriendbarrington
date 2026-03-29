using UnityEngine;
using FMODUnity;

public class SignPopUp : MonoBehaviour
{
    public Transform sign;

    public Vector3 startRotation = new Vector3(90f, 0f, 0f);
    public Vector3 overshootRotation = new Vector3(-10f, 0f, 0f); 
    public Vector3 backSwingRotation = new Vector3(5f, 0f, 0f); // new extra swing
    public Vector3 finalRotation = new Vector3(0f, 0f, 0f);

    public float popDuration = 0.4f;
    public float swingBackDuration = 0.35f;
    public float settleDuration = 0.4f;

    private float timer = 0f;
    private bool activated = false;
    private bool hasPlayed = false;

    private int phase = 0;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference popupSignEvent;
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference notifySignEvent;

    void Start()
    {
        if (sign != null)
            sign.eulerAngles = startRotation;
    }

    void Update()
    {
        if (!activated || hasPlayed) return;

        timer += Time.deltaTime;

        if (phase == 0)
        {
            float t = timer / popDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            sign.eulerAngles = Vector3.Lerp(startRotation, overshootRotation, t);

            if (timer >= popDuration)
            {
                timer = 0f;
                phase = 1;
            }
        }
        else if (phase == 1)
        {
            float t = timer / swingBackDuration;

            sign.eulerAngles = Vector3.Lerp(overshootRotation, backSwingRotation, t);

            if (timer >= swingBackDuration)
            {
                timer = 0f;
                phase = 2;
            }
        }
        else if (phase == 2)
        {
            float t = timer / settleDuration;

            sign.eulerAngles = Vector3.Lerp(backSwingRotation, finalRotation, t);

            if (timer >= settleDuration)
            {
                sign.eulerAngles = finalRotation;
                hasPlayed = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            activated = true;
            RuntimeManager.PlayOneShotAttached(popupSignEvent, gameObject);
            RuntimeManager.PlayOneShotAttached(notifySignEvent, gameObject);
        }
    }
}