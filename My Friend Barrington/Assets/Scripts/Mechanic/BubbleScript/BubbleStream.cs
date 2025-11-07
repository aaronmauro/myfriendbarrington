using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    private BoxCollider bxCollider;

    [SerializeField]
    private float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        //Debug.Log(player);

        if (other.gameObject.CompareTag("Player"))
        {
            player.inBubbleStream(force);
        }
    }
}
