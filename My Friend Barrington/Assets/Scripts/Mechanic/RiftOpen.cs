using UnityEngine;

public class RiftOpen : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private Transform targetObject;

    [Header("Scale Settings")]
    [SerializeField] private Vector3 smallScale = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 openScale = new Vector3(3f, 3f, 3f);

    [Header("Timing")]
    [SerializeField] private float openSpeed = 8f;
    [SerializeField] private float delayBeforeOpen = 0.1f;

    private bool isActive = false;
    private bool isOpening = false;
    private float timer;

    void Start()
    {
        // Start in small state
        if (targetObject != null)
            targetObject.localScale = smallScale;
    }

    void Update()
    {
        if (!isActive || targetObject == null) return;

        // Wait before opening
        if (!isOpening)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                isOpening = true;
            }
        }
        else
        {
            // Open (scale up)
            targetObject.localScale = Vector3.Lerp(
                targetObject.localScale,
                openScale,
                openSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            timer = delayBeforeOpen;
        }
    }
}