using UnityEngine;

public class TextScript : MonoBehaviour
{
    // getting video manager
    private TextManager tm;

    public int whenTextActive;
    public int whichVideoNumber;
    // Turn off on awake
    //private void Awake()
    //{

    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject tmFind = GameObject.Find("TextManager");
        tm = tmFind.GetComponent<TextManager>();
        tm.texts.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(VideoManager.adsNumber == whenTextActive);
        //Debug.Log(vm.videoCount == whichVideoNumber);
        //if (VideoManager.adsNumber == whenTextActive && vm.videoCount == whichVideoNumber)
        //{
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
