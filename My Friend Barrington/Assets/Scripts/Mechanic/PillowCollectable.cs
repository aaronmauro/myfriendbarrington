using UnityEngine;

public class PillowCollectable : MonoBehaviour
{
    private bool shrink = false;
    [SerializeField]
    private float shrinkFactor = 0.1f;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject.findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(shrink)
        {
            transform.localScale -= shrinkFactor * Vector3.one;
            if(transform.localScale.x <= 0)
            {
                player.GetComponent<Player>().isPushingBox = false; // i love being a good practice programmer - DV
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
