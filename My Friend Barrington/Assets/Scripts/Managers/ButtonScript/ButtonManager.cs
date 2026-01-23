using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


// Manages the activation and deactivation of buttons based on the current advertisement number.
public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    // Getting Buttons
    public List<List<GameObject>> buttons = new List<List<GameObject>>();
    public ButtonReturn buttonValue;
    [SerializeField]
    private int totalAdsNumber;
    public bool buttonStatus;
    public int numberOfChocie;
    // Getting Component
    private int totalAddition;
    private int videoAddition = 9; // control what video to play next

    [SerializeField]
    private VideoManager videoManager;
    //public InputActionReference skipVideo;

    // singleton this :>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        addButton();
    }
    // adding and removing skipping video methods
    private void OnEnable()
    {
        InputManager.GetInstance().videoSkipAction.action.performed += skipVideoMethod;
        InputManager.GetInstance().videoNextAction.action.performed += nextVideoMethod;
        InputManager.GetInstance().videoSkipAction.action.Enable();
        InputManager.GetInstance().videoNextAction.action.Enable();
    }

    private void OnDisable()
    {
        InputManager.GetInstance().videoSkipAction.action.performed -= skipVideoMethod;
        InputManager.GetInstance().videoNextAction.action.performed -= nextVideoMethod;
        InputManager.GetInstance().videoSkipAction.action.Disable();
        InputManager.GetInstance().videoNextAction.action.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonStatus = false;
        videoManager = gameObject.findVideoManager();
    }

    // Update is called once per frame
    void Update()
    {
        // show and hide button
        enableButton(buttonStatus);

        //Debug.Log(totalAddition);
    }
    // Changing Button active
    private void enableButton(bool isActive)
    {
        if (buttons[VideoManager.adsNumber].Count == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < buttons[VideoManager.adsNumber].Count; i++)
            {
                buttons[VideoManager.adsNumber][i].SetActive(isActive);
            }
        }
    }

    // adding button on the list
    private void addButton()
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

    // button to skip video
    private void skipVideoMethod(InputAction.CallbackContext context)
    {
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
    // playe next video method
    private void nextVideoMethod(InputAction.CallbackContext context)
    {
        videoManager.videoCount++;
        videoManager.afterLoopVideo = true;
        Debug.Log("Next Video: " + videoManager.videoCount);
    }

    // check witch video to play
    private void whatVideoToPlay(int num)
    {
        // create a list, adding the video scene number to the list pending to play;
        switch (num)
        {
            case 2:
                videoManager.newVideoList.Add(14);
                break;
            case 3:
                videoManager.newVideoList.Add(14);
                break;
            case 4:
                videoManager.newVideoList.Add(14);
                break;
            case 5:
                videoManager.newVideoList.Add(4);
                break;
            case 6:
                videoManager.newVideoList.Add(3);
                break;
            case 7:
                videoManager.newVideoList.Add(2);
                break;
/*            case 9:
                videoManager.videoCount = 1;
                break;*/
/*            default:
                videoManager.skipVideoCount = 1;
                break;*/
        }
    }

    public void calculateVideoNumber()
    {
        totalAddition = 0;
        for (int k = 0; k < buttons[VideoManager.adsNumber].Count; k++)
        {
            totalAddition += buttons[VideoManager.adsNumber][k].GetComponent<ButtonReturn>().buttonValue;
        }
        videoAddition = totalAddition;
        whatVideoToPlay(videoAddition);
    }
}
