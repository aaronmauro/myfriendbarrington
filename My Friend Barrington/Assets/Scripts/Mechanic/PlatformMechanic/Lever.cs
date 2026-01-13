
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // Inspector
    [SerializeField]
    public KeyCode interactButton;

    
    [SerializeField]
    private GameObject targetObject;
    private bool isTrigger;
    


    void Start()
    {
        isTrigger = false;
            if (targetObject == null)
            {
                targetObject.SetActive(false);
            }
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        
       
    }

    // Update is called once per frame
    void Update()
    {
     
        // Check for player interaction
        if (targetObject == null)
            {
                if (isTrigger && Input.GetKeyDown(interactButton))
                {
                targetObject.SetActive(false);
                }
            }
 
        else if (targetObject != null){
             if (isTrigger && Input.GetKeyDown(interactButton))
                {
                targetObject.SetActive(true);
                }
              
            }
    
        }
    
        

// When Player enters the lever collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            isTrigger = true;
        }
    }
    // When Player exits the lever collider
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.isPlayer())
        {
            isTrigger = false;
        }
    }

}
 