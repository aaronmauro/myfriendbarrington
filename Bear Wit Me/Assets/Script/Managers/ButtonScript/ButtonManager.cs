using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Getting Buttons
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();
    public bool buttonStatus;
    // Getting Component
    [SerializeField]
    private VideoManager videoManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonStatus = false;

        GameObject vm = GameObject.Find("VideoManager");
        videoManager = vm.GetComponent<VideoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enableButton();
    }

    private void enableButton()
    {
        if (buttonStatus)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(true);
            }
        }
        else if (!buttonStatus)
        {
            for (int j = 0; j < buttons.Count; j++)
            {
                buttons[j].SetActive(false);
            }
        }
    }
}
