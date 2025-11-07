
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // Inspector
    [SerializeField]
    public KeyCode interactButton;
    public Material targetMaterial;
    [SerializeField]
    Renderer objectRenderer;
    public Collider myCollider;
    
    [SerializeField]
    private GameObject targetObject;
    private bool isTrigger;
  

    void Start()
    {
        isTrigger = false;
       
    }

    // Update is called once per frame
    void Update()
    {
     
       
        if (isTrigger && Input.GetKeyDown(interactButton))
        {
            myCollider.enabled = false;

            if (objectRenderer != null && targetMaterial != null)
            {
                // Assign the new material
                objectRenderer.material = targetMaterial;
            }

        }
    }
        


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }

}
 