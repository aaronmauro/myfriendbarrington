using UnityEngine;
using Steamworks;

public class SteamAchieve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

     
    void Update()
    {
     if (!SteamManager.Initialized) { return; }

     if (!Input.GetKeyDown(KeyCode.F8)) { return; }

     SteamUserStats.ResetAllStats(true);   // use to reset stats when testing

    








     SteamUserStats.StoreStats();

    }
}
