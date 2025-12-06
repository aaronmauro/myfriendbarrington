using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    [SerializeField] float hookForce = 25f;

    Grapple grapple;
    Rigidbody rigid;
    LineRenderer lineRenderer;


    // Called by the Grapple to initialize the hook

    public void Initialize(Grapple grapple, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grapple = grapple;
        rigid = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DespawnIfNoContact(1f));
    }

    // Update is called once per frame
    void Update()
    {
        // Update the line renderer to draw the rope between the grapple and the hook
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            grapple.transform.position
        };

        lineRenderer.SetPositions(positions);



    }

    private bool hasLatched = false;

    // Called when the hook collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (hasLatched) return;

        if ((LayerMask.GetMask(GeneralGameTags.Grapple) & 1 << other.gameObject.layer) > 0)
        {
            hasLatched = true;
            rigid.useGravity = false;
            rigid.isKinematic = true;
            grapple.StartPull();
        }
    }

    // Despawn the hook if it hasn't latched onto anything within the timeout period

    private IEnumerator DespawnIfNoContact(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        // If the hook hasn't latched onto anything, destroy it
        if (!rigid.isKinematic)
        {
            Destroy(gameObject);
        }
    }






}
