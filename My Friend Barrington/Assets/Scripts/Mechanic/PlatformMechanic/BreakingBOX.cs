using UnityEngine;

public class BreakingBOX : MonoBehaviour
{
    // Tag assigned to the movable box object
    //public string boxTag = "MovableBox";
   
  
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the box tag
        if (collision.gameObject.CompareTag(GeneralGameTags.Box))
        {
            Destroy();          
        }
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
