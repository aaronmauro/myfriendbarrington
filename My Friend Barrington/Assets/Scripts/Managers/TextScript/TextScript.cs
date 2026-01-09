using UnityEngine;

public class TextScript : MonoBehaviour
{
    // getting video manager
    private TextManager tm;

    public int whenTextActive;
    public int whichVideoNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Adding itself to TextManager list
        GameObject tmFind = GameObject.Find(GeneralGameTags.TextManager);
        tm = tmFind.GetComponent<TextManager>();
        tm.texts.Add(gameObject);
    }
}
