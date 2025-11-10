using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// Manages the activation and deactivation of buttons based on the current advertisement number.
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
    private void Awake()
    {
        // Initialize button lists for each advertisement number
        while (buttons.Count <= totalAdsNumber)
        {
            buttons.Add(new List<GameObject>());
        }
        for (int j = 0; j < buttons[VideoManager.adsNumber].Count; j++)
        {
            buttons[VideoManager.adsNumber][j].SetActive(false);
        }
    }
    void Start()
    {
        buttonStatus = false;

        GameObject vm = GameObject.Find("VideoManager");
        videoManager = vm.GetComponent<VideoManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Enable or disable buttons based on buttonStatus
        enableButton();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("happy happy happy");
            //Debug.Log(VideoManager.adsNumber);
            //Debug.Log(buttons[VideoManager.adsNumber].Count == 0);
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
        // Enable buttons if buttonStatus is true
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
                // Disable buttons if buttonStatus is false
                for (int j = 0; j < buttons[VideoManager.adsNumber].Count; j++)
                {
                    buttons[VideoManager.adsNumber][j].SetActive(false);
                }
            }
        }
    }
}
