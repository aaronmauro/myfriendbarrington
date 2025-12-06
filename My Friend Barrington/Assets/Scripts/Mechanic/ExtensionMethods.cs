using UnityEngine;

public static class ExtensionMethods
{
    // new stuff new look
    public static bool isPlayer(this GameObject gameObject)
    {
        return gameObject.CompareTag(GeneralGameTags.Player);
    }

    // interesting, now i can do this all the time
    public static GameManager findGameManager(this GameObject gameObject)
    {
        GameObject findGameManagerObject = GameObject.Find(GeneralGameTags.GameManager);
        if (findGameManagerObject == null)
        {
            Debug.Log("Cannot find" + GeneralGameTags.GameManager);
            return null;
        }

        GameManager gm = findGameManagerObject.GetComponent<GameManager>();
        if (gm == null)
        {
            Debug.Log("Cannot find Script" + GeneralGameTags.GameManager);
            return null;
        }

        return gm;
    }
    // cool stuff, and i gonna use it many time :<
    public static VideoManager findVideoManager(this GameObject gameObject)
    {
        GameObject findGameObject = GameObject.Find(GeneralGameTags.VideoManager);
        if (findGameObject == null)
        {
            Debug.Log("Cannot find" + GeneralGameTags.VideoManager);
            return null;
        }

        VideoManager vm = findGameObject.GetComponent<VideoManager>();
        if (vm == null)
        {
            Debug.Log("Cannot find Script" + GeneralGameTags.VideoManager);
            return null;
        }

        return vm;
    }
    // player time
    public static Player findPlayer(this GameObject gameObject)
    {
        GameObject findPlayerObject = GameObject.Find(GeneralGameTags.Player);
        if (findPlayerObject == null)
        {
            Debug.Log("Cannot find" + GeneralGameTags.Player);
            return null;
        }

        Player player = findPlayerObject.GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Cannot find Script" + GeneralGameTags.Player);
            return null;
        }
        //Debug.Log()
        return player;
    }
}
