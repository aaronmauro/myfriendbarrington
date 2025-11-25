using UnityEngine;

public class Pillow : MonoBehaviour
{
    [SerializeField]
private float force;
private Player player;

    // When Player collides with the pillow
    private void OnCollisionEnter(Collision collision)
    {
        // Get Player component
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.isPlayer())
        {
            // Apply force to the player
            player.isPushed = true;
            //player.isPushedDirection(2, force); sorry changing your script because I changed the method
            player.pushingPlayer(Vector3.up, force);
           
        }
    }
}

