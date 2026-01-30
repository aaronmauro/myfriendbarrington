using UnityEngine;

public class AmimationShipTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "ShipFall";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("touched player");
                animator.SetTrigger(triggerName);
            }
        }
} 

