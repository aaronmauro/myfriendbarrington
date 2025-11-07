using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    [SerializeField] float hookForce = 25f;

    Grapple grapple;
    Rigidbody rigid;
    LineRenderer lineRenderer;




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
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            grapple.transform.position
        };

        lineRenderer.SetPositions(positions);



    }

    private bool hasLatched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasLatched) return;

        if ((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer) > 0)
        {
            hasLatched = true;
            rigid.useGravity = false;
            rigid.isKinematic = true;
            grapple.StartPull();
        }
    }


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
