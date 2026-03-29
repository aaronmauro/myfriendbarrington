using UnityEngine;
using Steamworks;

public class GrappleAchievementTracker : MonoBehaviour
{
    private const string AchievementID = "Hooking";
    private const string StatKey = "grapple_count";
    private const int RequiredCount = 20;

    private static int grappleCount = 0;

    private void Start()
    {
        if (!SteamManager.Initialized) return;

        // Load saved count from Steam stats
        SteamUserStats.GetStat(StatKey, out grappleCount);
        Debug.Log($"Grapple count loaded: {grappleCount}");
    }


    public static void RegisterGrapple()
    {
        if (!SteamManager.Initialized) return;

        // Check if already unlocked
        SteamUserStats.GetAchievement(AchievementID, out bool alreadyUnlocked);
        if (alreadyUnlocked) return;

        grappleCount++;
        Debug.Log($"Grapple used! Count: {grappleCount}/{RequiredCount}");

        // Save to Steam
        SteamUserStats.SetStat(StatKey, grappleCount);
        SteamUserStats.StoreStats();

        if (grappleCount >= RequiredCount)
        {
            SteamAchievementManager.UnlockAchievement(AchievementID);
        }
    }
}