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
    private Material[] materials;

    public enum pickMaterial
    {
        Pink,
        Blue,
        Purple,
        Red
    }

    public pickMaterial materialType;
    //private Color[] colors;


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
        if (materialType == pickMaterial.Pink)
        {
            colour.material = materials[0];
        }
        else if (materialType == pickMaterial.Blue)
        {
            colour.material = materials[1];
        }
        else if (materialType == pickMaterial.Purple)
        {
            colour.material = materials[2];
        }
        else if (materialType == pickMaterial.Red)
        {
            colour.material = materials[3];
        }
        //colour.material.color = colors[Random.Range(0, colors.Length)];
    }
}
