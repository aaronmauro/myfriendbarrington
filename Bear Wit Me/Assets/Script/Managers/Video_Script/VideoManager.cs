using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    // Inputing video clips
    [Header("Videos")]
    public VideosData[] ads1, ads2;
    // Getting Looping video number
    [Header("Video Number")]
    [SerializeField]
    public int[] videoToLoop;
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
    // Getting Component
    [Header("Component")]
    [SerializeField]
    private VideoPlayer videoPlayer;
    private ButtonManager bm;

    // Testing
    //[SerializeField]
    //private bool forcePlay;

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
            PlayVideo(Convert.ToString(videoCount));
            playNextVideo = false;
        }
        
        else if (afterLoopVideo /*forcePlay*/)
        {
            PlayVideo(Convert.ToString(videoCount));
            playNextVideo = false;
        }
         // working later to break the video and straight to next one
        // Calling methods
        checkVideoLoop();
        checkVideoStatus();
    }
    // Method to play ads1
    public void PlayVideo(string videoName)
    {
        // Finding the name in the array
        VideosData v = Array.Find(ads1, x => x.Name == videoName);
        // If the program cannot find it
        if (v == null)
        {
            Debug.Log("Entered Wrong Name");
        }
        // Set the clip and play the video
        else
        {
            videoPlayer.clip = v.videoClip;
            videoPlayer.Play();
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
            playNextVideo = true;
            // Check if video need to be loop
            if (!loopVideo)
            {
                videoCount += 1 - videoControlNumber;
            }
            videoControlNumber = 0;
            afterLoopVideo = false;
        }
    }
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
            else if (videoToLoop[i] <= videoCount)
            {
                // Change scene script here
            }
        }
    }
}
