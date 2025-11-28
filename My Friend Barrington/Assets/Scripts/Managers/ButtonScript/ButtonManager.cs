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
    [SerializeField]
    public List<List<GameObject>> buttons = new List<List<GameObject>>();
    [SerializeField]
    private int totalAdsNumber;
    // Getting Component
    [SerializeField]
    private VideoManager videoManager;

    public InputActionReference skipVideo;
    //public delegate void ButtonStatusDelegate(ButtonManager buttonManager);
    //ButtonStatusDelegate buttonStatusDelegate;

    //[SerializeField]
    //private bool ButtonStatus;
    public bool buttonStatus;
    public bool _ButtonStatus
    {
        get { return buttonStatus; }
        set 
        {
            Debug.Log(value);
            if (buttonStatus == value) 
            {
                Debug.Log("not happpy");
                enableButton(false);
                return;
            }

            buttonStatus = value;

            // Enable or disable buttons based on buttonStatus
            enableButton(true);
            //enableButton();
            //buttonStatusDelegate(this);
        }
    }
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

    private void OnEnable()
    {
        skipVideo.action.performed += skipVideoMethod;
        skipVideo.action.Enable();
    }

    private void OnDisable()
    {
        skipVideo.action.performed -= skipVideoMethod;
        skipVideo.action.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonStatus = false;
        //enableButton(false);

        GameObject vm = GameObject.Find(GeneralGameTags.VideoManager);
        videoManager = vm.GetComponent<VideoManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_ButtonStatus);
        //Debug.Log(buttonStatus);
        //if (skipVideo.action.triggered)
        //{
        //    //Debug.Log("happy happy happy");
        //    //Debug.Log(VideoManager.adsNumber);
        //    //Debug.Log(buttons[VideoManager.adsNumber].Count == 0);
             
        //}
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
}
