using UnityEngine;

public class Pillow : MonoBehaviour
{
    [SerializeField]
private float force;
private Player player;


    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isPushed = true;
            player.isPushedDirection(2, force);
           
        }
    }
}

