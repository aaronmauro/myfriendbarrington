using TMPro;
using UnityEngine;
using UnityEngine.UI;


//A point in the world that the player can grapple to.
public class GrapplePoint : MonoBehaviour
{
    public float activationRange = 20f;

    [SerializeField]
    private Image fillImage;
    private float fillAmount;
    private Vector3 testing;

    // Draw Gizmos
    private Color gizColor = Color.yellow;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find(GeneralGameTags.Player);
    }

    private void Update()
    {
        if (fillImage != null)
        {
            fillAmount = 1 - Vector3.Distance(transform.position, player.transform.position)/100f;
            //Debug.Log(fillAmount);

            fillImage.fillAmount = fillAmount;
        }
    }

    public bool IsInRange(Vector3 playerPosition)
    {
        return Vector3.Distance(transform.position, playerPosition) <= activationRange;
    }
    // Draw Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = gizColor;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}