using UnityEngine;

public class Damage : MonoBehaviour
{
    // Component
    private GameManager gm;

    private void Start()
    {
        GameObject obj = GameObject.Find("GameManager");
        gm = obj.GetComponent<GameManager>();
    }

    // When player collide with damage object 
    private void OnCollisionEnter(Collision collision)
    {
        // If collision to player tag
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameManager gm = GetComponent<GameManager>();
            // back to spawn
            gm.backToSpawn = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // If collision stay with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // back to spawn
            gm.backToSpawn = true;
        }
    }
}
