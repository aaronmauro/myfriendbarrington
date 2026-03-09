using UnityEngine;

public class PopUpRotate: MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasPlayed) return;
        Debug.Log("Animation Played!");
        animator.SetTrigger("Play");
        hasPlayed = true;
    }
}