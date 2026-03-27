using UnityEngine;
using Steamworks;

public class SteamAchieve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!SteamManager.Initialized) { return; }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        SteamUserStats.ResetAllStats(true);   // use to reset stats when testing

        //SteamUserStats.SetAchievement("BEAR_TESTING");

        //SteamUserStats.SetAchievement("Hooking");

        //SteamUserStats.SetAchievement("NPCM");

        //SteamUserStats.SetAchievement("NPCP");

        //SteamUserStats.SetAchievement("NPCF");

        //.SetAchievement("Lvl_1");

        //SteamUserStats.SetAchievement("Lvl_2");

        //SteamUserStats.SetAchievement("Lvl_3");

        //SteamUserStats.SetAchievement("Find_Marty");









        SteamUserStats.StoreStats();

    }
}
