using UnityEngine;

public class Bubbles : MonoBehaviour
{
    // Destory when hit player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
