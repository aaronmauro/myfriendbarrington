using UnityEngine;
using TMPro;

public class ButtonCollect : MonoBehaviour
{
    // Shared across all instances
    public static int buttonCount = 0;

    [SerializeField]
    public GameObject button;

    public TMP_Text messageText;

    void Start()
    {
        messageText = GameObject.Find("messageText").GetComponent<TMP_Text>(); // if our game is ever super laggy on start, this is why - DV
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer() && button != null)
        {
            // Play Coin Sound Here
            //AudioManager.instance.playSFX("Coin");


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
