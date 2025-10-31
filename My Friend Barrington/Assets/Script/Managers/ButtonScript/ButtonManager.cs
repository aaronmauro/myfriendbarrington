using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Getting Buttons
    [SerializeField]
    public List<List<GameObject>> buttons = new List<List<GameObject>>();
    public bool buttonStatus;
    [SerializeField]
    private int totalAdsNumber;
    // Getting Component
    [SerializeField]
    private VideoManager videoManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonStatus = false;

        GameObject vm = GameObject.Find("VideoManager");
        videoManager = vm.GetComponent<VideoManager>();
        while (buttons.Count <= totalAdsNumber)
        {
            buttons.Add(new List<GameObject>());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        enableButton();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("happy happy happy");
            //Debug.Log(VideoManager.adsNumber);
            if (VideoManager.adsNumber == 0)
            {
                videoManager.videoCount = videoManager.ads1.Length;
                videoManager.afterLoopVideo = true;
            }
            else if (VideoManager.adsNumber == 1)
            {
                videoManager.videoCount = videoManager.ads2.Length;
                videoManager.afterLoopVideo = true;
            }
        }
    }
    // Changing Button active
    private void enableButton()
    {
        if (buttonStatus)
        {
            if (buttons[VideoManager.adsNumber].Count == 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < buttons[VideoManager.adsNumber].Count; i++)
                {
                    buttons[VideoManager.adsNumber][i].SetActive(true);
                }
            }
        }
        else if (!buttonStatus)
        {
            if (buttons[VideoManager.adsNumber].Count == 0)
            {
                return;
            }
            else
            {
                for (int j = 0; j < buttons[VideoManager.adsNumber].Count; j++)
                {
                    buttons[VideoManager.adsNumber][j].SetActive(false);
                }
            }    
        }
    }
}
