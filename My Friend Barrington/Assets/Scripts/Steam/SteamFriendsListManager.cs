using UnityEngine;
using Steamworks;
using System.Collections.Generic;

public class SteamFriendsListManager : MonoBehaviour
{
    // A list to store the CSteamIDs of the user's friends
    private List<CSteamID> friendsList = new List<CSteamID>();

    void Start()
    {
        if (SteamManager.Initialized)
        {
            Debug.Log("Steam Manager initialized. Getting friends list...");
            GetFriendsList();
        }
        else
        {
            Debug.LogError("Steam Manager not initialized. Make sure Steamworks.NET is set up correctly and Steam is running.");
        }
    }

    public void GetFriendsList()
    {
        friendsList.Clear();

        // Get the number of friends the user has
        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
        Debug.Log("Found " + friendCount + " friends.");

        for (int i = 0; i < friendCount; i++)
        {
            // Get the Steam ID of each friend
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            friendsList.Add(friendSteamID);

            // You can also retrieve more information, such as their name and presence
            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
            EPersonaState friendState = SteamFriends.GetFriendPersonaState(friendSteamID);

            Debug.Log($"Friend {i}: {friendName} ({friendSteamID.m_SteamID}), Status: {friendState}");
        }

        // Example of what to do next: maybe update a UI list of friends
        UpdateFriendsUI();
    }

    private void UpdateFriendsUI()
    {
        // Add code here to populate your Unity UI with the friendsList data.
        // For example, displaying their names and whether they are online.
    }

    // Example of how to use the Steam Overlay to invite friends to a lobby
    public void OpenInviteOverlay(CSteamID lobbyID)
    {
        // Note: This often does not work in the Unity editor and needs a build to test properly
        SteamFriends.ActivateGameOverlayInviteDialog(lobbyID);
    }
}