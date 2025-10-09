using TMPro;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    // Inspector
    [SerializeField]
    private TextMeshPro interactText;
    public KeyCode interactButton;

    [SerializeField]
    private GameObject targetObject;
    private bool isTrigger;
    private bool isHolding;
    [SerializeField]
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTrigger = false;
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            gameObject.transform.position = targetObject.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(interactButton))
            {
                if (!isHolding)
                {
                    isTrigger = true;
                    isHolding = true;
                }
                else if (isHolding)
                {
                    isTrigger = false;
                    isHolding = false;
                }
            }
        }
    }
}
