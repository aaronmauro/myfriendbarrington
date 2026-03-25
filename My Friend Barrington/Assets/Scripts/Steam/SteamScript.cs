using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    private void Start()
    {
        if (!SteamManager.Initialized) { return; }

        string name = SteamFriends.GetPersonaName();
        
        Debug.Log(name);
    }
}
