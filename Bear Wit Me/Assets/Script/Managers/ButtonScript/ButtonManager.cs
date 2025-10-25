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
    }
    // Changing Button active
    private void enableButton()
    {
        if (buttonStatus)
        {
            for (int i = 0; i < buttons[videoManager.adsNumber].Count; i++)
            {
                buttons[videoManager.adsNumber][i].SetActive(true);
            }
        }
        else if (!buttonStatus)
        {
            for (int j = 0; j < buttons[videoManager.adsNumber].Count; j++)
            {
                buttons[videoManager.adsNumber][j].SetActive(false);
            }
        }
    }
}
