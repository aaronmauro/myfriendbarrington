using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    // Getting Player
    [SerializeField]
    private Player player;
    // Checking for collison to ground
    private void OnTriggerEnter(Collider other)
    {
        // Getting player script
        //Player player = GetComponent<Player>();
        if (other.gameObject.layer == 10)
        {
            // Sending true statement back to player script
            player.isGround = true;
        }
    }
}
