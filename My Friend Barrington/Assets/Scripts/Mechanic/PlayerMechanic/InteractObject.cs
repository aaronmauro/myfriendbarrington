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
        // Show or hide the interact text based on whether the player is in range
        if (isTrigger && Input.GetKeyDown(interactButton))
        {
            isPressed();
        }
        //Debug.Log(isHolding);
        if (isHolding)
        {
            gameObject.transform.position = targetObject.transform.position;
        }
    }

    // Detect when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            isTrigger = true;
        }
    }

    // Detect when the player exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.isPlayer())
        {
            isTrigger = false;
        }
    }

    // Toggle the holding state when the interact button is pressed
    private void isPressed()
    {
        isHolding = !isHolding;
        //Debug.Log(isHolding);
    }
}
