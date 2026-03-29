using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class FallingPlatform : MonoBehaviour
{
    private float fallSpeed;
    private float distanceTravel;
    private Vector3 towardTransition;
    private bool isFalling;

      [Header("Audio (FMOD)")]
    [SerializeField] private EventReference rocksFallEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            fallSpeed += Time.deltaTime / 10f;
            transform.position += Vector3.down * fallSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
/*        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && !collision.gameObject.isPlayer())
        {
            Rigidbody rb;
            if (!TryGetComponent(out rb))
            {
                Debug.LogError($"{name} has no Rigidbody component!");
                return;
            }

            rb.mass = 1000f;
        }*/
        if (collision.gameObject.isPlayer())
        {
            isFalling = true;
            RuntimeManager.PlayOneShotAttached(rocksFallEvent, gameObject);
            //Invoke("StartFalling", 0.5f);
        }
    }

/*    private void StartFalling()
    {
        if (isFalling)
        {
            Rigidbody rb;
            if (!TryGetComponent(out rb))
            {
                Debug.LogError($"{name} has no Rigidbody component!");
                return;
            }


        }
    }*/
}
