using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;
using JetBrains.Annotations;

public class VideoManager : MonoBehaviour
{
    // Inputing video clips
    [Header("Videos")]
    public VideosData[] ads1, ads2;
    private VideosData _v;
    public List<int> newVideoList = new List<int>(new int[8]);
    public int newVideoCount;
    public static int adsNumber;
    // Getting Looping video number
    [Header("Video Number")]
    //[SerializeField]
    [HideInInspector]
    public bool loopVideo;
    [HideInInspector]
    public int videoCount;
    private int videoControlNumber;
    [HideInInspector]
    public bool afterLoopVideo;
    // Video status
    private long currentFrame;
    private long videoFrame;
    [HideInInspector]
    public bool playNextVideo;
    private bool isPauseVideo;
    // Change Scene
    public string[] nextSceneName;
    public KeyCode inputKey;
    public int skipVideoCount;

    // remote raw image
    [SerializeField]
    private GameObject remoteImage;
    private StudioEventEmitter fmodEventEmitter; 

    // Getting Component
    [Header("Component")]
    [SerializeField]
    private VideoPlayer videoPlayer;
    private ButtonManager bm;

    private void Awake()
    {
        if (FindFirstObjectByType<AudioManager>() != null)
        {
            //Destroy(FindFirstObjectByType<AudioManager>());
            AudioManager.instance.bgSFX.Stop();
        }

        fmodEventEmitter = GameObject.Find("FMODEvent").GetComponent<StudioEventEmitter>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting Component when starting game
        videoPlayer = GetComponent<VideoPlayer>();
        GameObject buttonManager = GameObject.Find(GeneralGameTags.ButtonManager);
        bm = buttonManager.GetComponent<ButtonManager>();

        // Adding list
        newVideoList.Add(0);
        newVideoList.Add(1);

        // Play video
        playNextVideo = true;
        videoCount = 0;
        newVideoCount = 0;
        videoControlNumber = 0;
        remoteImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Play next video
        if (playNextVideo)
        {
            playVideo(Convert.ToString(videoCount));
            playNextVideo = false;
        }
        
        else if (afterLoopVideo /*forcePlay*/)
        {
            playVideo(Convert.ToString(videoCount));
            playNextVideo = false;
        }

        if (isPauseVideo)
        {
            if (Input.GetKeyDown(inputKey))
            {
                //videoCount += 1;
                isPauseVideo = false;
                loopVideo = false;
            }
        }

        // working later to break the video and straight to next one
        // Calling methods
        checkVideoStatus();
        //Debug.Log(skipVideoCount);
        //Debug.Log(videoCount);
        //Debug.Log(newVideoCount);
        //Debug.Log(_v);

    }
    // Method to play ads1
    public void playVideo(string videoName)
    {
        // Finding the name in the array
        if (adsNumber == 0)
        {
            _v = Array.Find(ads1, x => x.Name == Convert.ToString(newVideoList[newVideoCount]));
        }
        else if (adsNumber == 1)
        {
            _v = Array.Find(ads2, x => x.Name == videoName);
        }
        // If the program cannot find it
        if (_v == null)
        {
            // change scene when ran out in the list
            SceneManagerScript.instance.nextScene(nextSceneName[adsNumber]);
            adsNumber += 1;
        }
        // Set the clip and play the video
        else
        {
            videoPlayer.clip = _v.videoClip;
            videoPlayer.Play();
            remoteImage.SetActive(false);

            // Initialize FMOD walk event instance from EventReference
            
            fmodEventEmitter.EventReference = _v.videoAudio;

            fmodEventEmitter.Play();
          

            if (_v.isLoop)
            {
                loopVideo = true;
                bm.buttonStatus = true;
                remoteImage.SetActive(true);
                if (_v.isSmashingButton)
                {
                    isPauseVideo = true;
                }
            }
            else if (_v.isSmashingButton)
            {
                isPauseVideo = true;
            }
        }
    }
    // Method to check video status
    private void checkVideoStatus()
    {
        // Getting current frame and the video length
        currentFrame = videoPlayer.frame;
        videoFrame = Convert.ToInt64(videoPlayer.frameCount);
        if (afterLoopVideo)
        {
            videoControlNumber = 1;
        }
        // Compare to check if the video ended
        if (currentFrame >= videoFrame - 1)
        {
            if ((loopVideo && isPauseVideo) || !isPauseVideo)
            {
                //newVideoCount++;
                playNextVideo = true;
            }
            // Check if video need to be loop
            if (!loopVideo && !isPauseVideo)
            {
                newVideoCount += 1 - videoControlNumber;
                videoCount += 1 - videoControlNumber;
            }
            videoControlNumber = 0;
            afterLoopVideo = false;
        }
    }
}
