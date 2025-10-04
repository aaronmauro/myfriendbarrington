using UnityEngine;

public class Damage : MonoBehaviour
{
    // Component
    private GameManager gm;

    // When player collide with damage object 
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = GameObject.Find("GameManager");
        gm = obj.GetComponent<GameManager>();
        // If collision to player tag
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameManager gm = GetComponent<GameManager>();
            // back to spawn
            gm.backToSpawn = true;
        }
    }
}
