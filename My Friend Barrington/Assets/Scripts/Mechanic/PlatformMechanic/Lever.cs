using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] 
    private KeyCode interactButton = KeyCode.E;
    [SerializeField] 
    private GameObject targetObject;


    [SerializeField] 
    private bool startEnabled = false;

    [SerializeField] 
    private bool isUsed;

    [SerializeField]
    private ParticleSystem interactParticles;

    private bool isTrigger;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(startEnabled);
    }

    void Update()
    {
        if (isTrigger && targetObject != null && Input.GetKeyDown(interactButton) && isUsed == false)
        {
            
            targetObject.SetActive(!targetObject.activeSelf);
            isUsed = true;

            if (interactParticles != null)
                interactParticles.Play();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isTrigger = false;
    }
}
