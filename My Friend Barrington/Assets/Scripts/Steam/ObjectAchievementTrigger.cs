using UnityEngine;
using Steamworks;

public class ObjectAchievementTrigger : MonoBehaviour
{
    [SerializeField] private string achievementID = "Lvl_1";
    [SerializeField] private bool destroyOnTouch = false; // Optional: remove object after trigger

    private bool unlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag("Player"))
        {
            SteamAchievementManager.UnlockAchievement(achievementID);
            unlocked = true;

            if (destroyOnTouch)
                Destroy(gameObject);
        }
    }
}