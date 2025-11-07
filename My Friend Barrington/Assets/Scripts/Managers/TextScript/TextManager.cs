using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    // Getting list of UI text
    [SerializeField]
    public List<GameObject> texts = new List<GameObject>();
    private TextScript ts;

    private VideoManager vm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject findVM = GameObject.Find("VideoManager");
        vm = findVM.GetComponent<VideoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            ts = texts[i].GetComponent<TextScript>();
            if (VideoManager.adsNumber == ts.whenTextActive && vm.videoCount == ts.whichVideoNumber)
            {
                ts.gameObject.SetActive(true);
            }
            else
            {
                ts.gameObject.SetActive(false);
            }
        }
    }
}
