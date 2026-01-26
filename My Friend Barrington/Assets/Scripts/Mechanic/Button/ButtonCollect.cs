using UnityEngine;
using TMPro;

public class ButtonCollect : MonoBehaviour
{
    // Shared across all instances
    public static int buttonCount = 0;

    [SerializeField]
    public GameObject button;

    [SerializeField]
    public int buttonWorth;

    public TMP_Text messageText;
    private Renderer colour;
    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        colour = GetComponent<Renderer>();
        changeColour();
        messageText = GameObject.Find("messageText").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.isPlayer() && button != null)
        {
            // Play Coin Sound Here
            //AudioManager.instance.playSFX("Coin");

            button.SetActive(false);

            buttonCount = buttonCount + buttonWorth;        // increases the global count
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        messageText.text = "x" + buttonCount;
    }

    private void changeColour()
    {
        colour.material.color = colors[Random.Range(0, colors.Length)];
    }
}
