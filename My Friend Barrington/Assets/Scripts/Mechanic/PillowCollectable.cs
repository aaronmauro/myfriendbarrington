using UnityEngine;

public class PillowCollectable : MonoBehaviour
{
    private bool shrink = false;
    [SerializeField]
    private float shrinkFactor = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shrink)
        {
            transform.localScale -= shrinkFactor * Vector3.one;
            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Collect()
    {
        shrink = true;
        Destroy(gameObject.GetComponent<BoxCollider>());
    }
}
