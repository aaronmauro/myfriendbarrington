using UnityEngine;
using Steamworks;

public class NPCAchievementTrigger : MonoBehaviour
{
    [SerializeField] private string achievementID = "NPCM";

    private bool unlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (unlocked) return;

        if (other.CompareTag("Player"))
        {
            SteamAchievementManager.UnlockAchievement(achievementID);
            unlocked = true; // Prevents triggering again this session
        }
    }
}