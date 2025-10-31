using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    // Inputing video clips
    [Header("Videos")]
    public VideosData[] ads1, ads2;
    private VideosData _v;
    public static int adsNumber;
    // Getting Looping video number
    [Header("Video Number")]
    //[SerializeField]
    //public int[] videoToLoop;
    [HideInInspector]
    public bool loopVideo;
    [HideInInspector]
    public int videoCount;
    //private int videoCountCheck;
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
    // Getting Component
    [Header("Component")]
    [SerializeField]
    private VideoPlayer videoPlayer;
    private ButtonManager bm;

    // Testing
    //[SerializeField]
    //private bool forcePlay;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting Component when starting game
        videoPlayer = GetComponent<VideoPlayer>();
        GameObject buttonManager = GameObject.Find("ButtonManager");
        bm = buttonManager.GetComponent<ButtonManager>();

        // Play video
        playNextVideo = true;
        videoCount = 0;
        //videoCountCheck = 0;
        videoControlNumber = 0;
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
        //Debug.Log(adsNumber);
        //Debug.Log(ads1.Length);
        //Debug.Log(isPauseVideo);
        //Debug.Log(videoCount);
        // working later to break the video and straight to next one
        // Calling methods
        //checkVideoLoop();
        checkVideoStatus();
        //Debug.Log(Array.Find(ads2, x => x.isLoop == true));
        //Debug.Log(ads1);
    }
    // Method to play ads1
    public void playVideo(string videoName)
    {
        // Finding the name in the array
        if (adsNumber == 0)
        {
            _v = Array.Find(ads1, x => x.Name == videoName);
        }
        else if (adsNumber == 1)
        {
            _v = Array.Find(ads2, x => x.Name == videoName);
        }
        // If the program cannot find it
        if (_v == null)
        {
            //Debug.Log("Entered Wrong Name");
            // change scene when ran out in the list
            adsNumber += 1;
            SceneManagerScript.instance.nextScene(nextSceneName[adsNumber]);
        }
        // Set the clip and play the video
        else
        {
            videoPlayer.clip = _v.videoClip;
            videoPlayer.Play();
            if (_v.isLoop)
            {
                loopVideo = true;
                bm.buttonStatus = true;
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
                playNextVideo = true;
            }
            // Check if video need to be loop
            if (!loopVideo && !isPauseVideo)
            {
                videoCount += 1 - videoControlNumber;
            }
            videoControlNumber = 0;
            afterLoopVideo = false;
        }
    }
    /*
    // Check if the video need to loop
    private void checkVideoLoop()
    {
        // Check if loop video input same as current video 
        for (int i = 0; i < videoToLoop.Length; i++)
        {
            if (videoToLoop[i] == videoCount)
            {
                loopVideo = true;
                bm.buttonStatus = true;
            }
        }
    }
    */
}
