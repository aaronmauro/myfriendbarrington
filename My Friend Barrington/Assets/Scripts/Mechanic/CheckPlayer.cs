using UnityEngine;

public static class CheckPlayer 
{
    public static bool isPlayer(this GameObject gameObject)
    {
        return gameObject.CompareTag(GeneralGameTags.Player);
    }
}
