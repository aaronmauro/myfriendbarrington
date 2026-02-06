using UnityEngine;

public class ShipCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private ShipCutsceneController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.PlayCutscene(other.gameObject);
        }
    }
}