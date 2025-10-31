using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField]
    private float force;
    private Player player;

    private void Start()
    {
        //GameObject findPlayer = GameObject.Find("Player");
        //player = findPlayer.GetComponent<Player>();
    }
    // Destory when hit player
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isPushed = true;
            player.isPushedDirection(2, force);
            Destroy(gameObject,2f);
        }
    }
}
