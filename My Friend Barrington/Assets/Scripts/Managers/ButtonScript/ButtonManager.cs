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
    [SerializeField]
    private int totalAdsNumber;
    public bool buttonStatus;
    // Getting Component
    [SerializeField]
    private VideoManager videoManager;
    public InputActionReference skipVideo;

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
        videoManager = gameObject.findVideoManager();
    }

    // Update is called once per frame
    void Update()
    {
        // show and hide button
        enableButton(buttonStatus);
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
}
