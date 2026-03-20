using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A point in the world that the player can grapple to.
public class GrapplePoint : MonoBehaviour
{
    public float activationRange = 20f;
    [SerializeField] GameObject indicator; // NEW - assign your Canvas/Image in the Inspector
    //[SerializeField] private Image fillImage;
    private float fillAmount;
    private Vector3 testing;
    private Color gizColor = Color.yellow;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find(GeneralGameTags.Player);
    }

    private void Update()
    {
        /* if (fillImage != null)
        {
            fillAmount = 1 - Vector3.Distance(transform.position, player.transform.position)/100f;
            fillImage.fillAmount = fillAmount;
        }*/
    }

    // NEW
    public void ShowIndicator() => indicator.SetActive(true);
    public void HideIndicator() => indicator.SetActive(false);

    // NEW
   

    public bool IsInRange(Vector3 playerPosition)
    {
        return Vector3.Distance(transform.position, playerPosition) <= activationRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizColor;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}