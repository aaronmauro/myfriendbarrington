using UnityEngine;
using TMPro;

public class ButtonCollect : MonoBehaviour
{
    // Shared across all instances
    public static int buttonCount = 0;

    [SerializeField]
    public GameObject button;

    public TMP_Text messageText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer() && button != null)
        {
            button.SetActive(false);

            buttonCount++;        // increases the global count
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        messageText.text = "x" + buttonCount;
    }
}
