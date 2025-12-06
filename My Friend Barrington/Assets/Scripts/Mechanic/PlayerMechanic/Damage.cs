    using UnityEngine;

public class Damage : MonoBehaviour
{
    // Component
    private GameManager gm;

    private void Start()
    {
        GameObject obj = GameObject.Find(GeneralGameTags.GameManager);
        gm = obj.GetComponent<GameManager>();
    }

    // When player collide with damage object 
    private void OnCollisionEnter(Collision collision)
    {
        // If collision to player tag
     
        if (collision.gameObject.isPlayer())
        {
            //GameManager gm = GetComponent<GameManager>();
            // back to spawn
            //gm.backToSpawn = true;
            gm.respawn();
        }
   
    }

    private void OnCollisionStay(Collision collision)
    {
        // If collision stay with player
        if (collision.gameObject.isPlayer())
        {
            // back to spawn
            //gm.backToSpawn = true;
            gm.respawn();
        }
    }
}
