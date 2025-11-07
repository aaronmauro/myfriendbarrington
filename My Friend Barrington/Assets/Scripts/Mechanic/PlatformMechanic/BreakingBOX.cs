using UnityEngine;

public class BreakingBOX : MonoBehaviour
{
    public string boxTag = "MovableBox";
   
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(boxTag))
        {
            Destroy();          
        }
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
