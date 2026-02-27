using UnityEngine;
using TMPro;
using FMODUnity;

public class ButtonCollect : MonoBehaviour
{
    public static int buttonCount = 0;

    [Header("Button Settings")]
    [SerializeField] private GameObject button;
    [SerializeField] private int buttonWorth;

    [Header("UI")]
    [SerializeField] private TMP_Text messageText;   

    [Header("Visuals")]
    [SerializeField] private Material[] materials;
    private Renderer colour;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference coinCollectEvent;

    public enum pickMaterial { Pink, Blue, Purple, Red }
    public pickMaterial materialType;

    private void Start()
    {
        // Renderer safety check
       // if (!TryGetComponent(out colour))
       // {
         //   Debug.LogError($"{name} has no Renderer component!");
          //  return;
      //  }

        

        // UI safety check
        if (messageText == null)
        {
            Debug.LogError("MessageText is NOT assigned in the inspector!");
        }
    }
    
    private void OnValidate()
    {
            // Renderer safety check
        if (!TryGetComponent(out colour))
        {
            Debug.LogError($"{name} has no Renderer component!");
            return;
        }

            changeColour();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.isPlayer() || button == null)
            return;

        button.SetActive(false);
        buttonCount += buttonWorth;
        UpdateScore();
        RuntimeManager.PlayOneShotAttached(coinCollectEvent, gameObject);
    }

    private void UpdateScore()
    {
        if (messageText != null)
            messageText.text = "x" + buttonCount;
    }

    private void changeColour()
    {   colour.material = materialType switch
        {
            pickMaterial.Pink => materials[0],
            pickMaterial.Blue => materials[1],
            pickMaterial.Purple => materials[2],
            pickMaterial.Red => materials[3],
            _ => colour.material
        };
     
    }
}
