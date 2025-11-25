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

    // When the bubble collides with the player, apply a pushing force and destroy the bubble after 2 seconds
    private void OnCollisionEnter(Collision collision)
    {
        // getting player
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.isPlayer())
        {
            player.isPushed = true;
            //player.isPushedDirection(2, force);
            // pushing player;
            player.pushingPlayer(Vector3.up, force);
            Destroy(gameObject,2f);
        }
    }
}
