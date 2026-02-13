using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;

public class VideoManager : MonoBehaviour
{
    // Inputing video clips
    [Header("Videos")]
    public VideosData[] ads1, ads2;
    private VideosData _v;
    public List<int> newVideoList = new List<int>(new int[8]);
    public int newVideoCount;
    public static int adsNumber;

    [Header("Video Number")]
    [HideInInspector] public bool loopVideo;
    [HideInInspector] public int videoCount;
    private int videoControlNumber;
    [HideInInspector] public bool afterLoopVideo;

    // Video status
    private long currentFrame;
    private long videoFrame;
    [HideInInspector] public bool playNextVideo;
    private bool isPauseVideo;

    // Change Scene
    public string[] nextSceneName;
    public KeyCode inputKey;
    public int skipVideoCount;

    [SerializeField] private GameObject remoteImage;
    private StudioEventEmitter fmodEventEmitter;

    [Header("Component")]
    [SerializeField] private VideoPlayer videoPlayer;
    private ButtonManager bm;

    private void Awake()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.bgSFX.Stop();
        }

        // fmod - ensure the object name matches your hierarchy
        GameObject fmodObj = GameObject.Find("FMODEvent");
        if (fmodObj != null)
        {
            fmodEventEmitter = fmodObj.GetComponent<StudioEventEmitter>();
        }

        // Loading resources (Keep your existing paths)
        LoadVideoClips();
    }

    void Start()
    {
        videoPlayer = GameObject.Find("VideoCanvas").GetComponent<VideoPlayer>();
        GameObject buttonManager = GameObject.Find(GeneralGameTags.ButtonManager);
        bm = buttonManager.GetComponent<ButtonManager>();

        newVideoList.Add(0);
        newVideoList.Add(1);

        playNextVideo = true;
        videoCount = 0;
        newVideoCount = 0;
        videoControlNumber = 0;
        remoteImage.SetActive(false);

        fmodEventEmitter.Play();

    }

    void Update()
    {
        if (playNextVideo || afterLoopVideo)
        {
            playVideo(Convert.ToString(videoCount));
            playNextVideo = false;
        }

        if (isPauseVideo)
        {
            if (Input.GetKeyDown(inputKey))
            {
                isPauseVideo = false;
                loopVideo = false;
            }
        }

        checkVideoStatus();
    }

    public void playVideo(string videoName)
    {
        if (adsNumber == 0)
        {
            _v = Array.Find(ads1, x => x.Name == Convert.ToString(newVideoList[newVideoCount]));
        }
        else if (adsNumber == 1)
        {
            _v = Array.Find(ads2, x => x.Name == videoName);
        }

        if (_v == null)
        {
            SceneManagerScript.instance.nextScene(nextSceneName[adsNumber]);
            adsNumber += 1;
        }
        else
        {
            // --- VIDEO PLAYBACK ---
            videoPlayer.clip = _v.videoClip;
            videoPlayer.Play();
            remoteImage.SetActive(false);

            // --- FMOD AUDIO SWAP ---
            if (fmodEventEmitter != null)
            {
                // Stop previous audio immediately
                //fmodEventEmitter.Stop();

                // Update the event reference from your VideosData scriptable/struct
                fmodEventEmitter.EventReference = _v.videoAudio;

                // Play the new event
                //fmodEventEmitter.Play();
            }

            // --- LOGIC CHECKS ---
            if (_v.isLoop)
            {
                loopVideo = true;
                bm.buttonStatus = true;
                remoteImage.SetActive(true);
                if (_v.isSmashingButton) isPauseVideo = true;
            }
            else if (_v.isSmashingButton)
            {
                isPauseVideo = true;
            }
        }
    }

    private void checkVideoStatus()
    {
        currentFrame = videoPlayer.frame;
        videoFrame = Convert.ToInt64(videoPlayer.frameCount);

        if (afterLoopVideo) videoControlNumber = 1;

        if (currentFrame >= videoFrame - 1)
        {
            if ((loopVideo && isPauseVideo) || !isPauseVideo)
            {
                playNextVideo = true;
            }
            if (!loopVideo && !isPauseVideo)
            {
                newVideoCount += 1 - videoControlNumber;
                videoCount += 1 - videoControlNumber;
            }
            videoControlNumber = 0;
            afterLoopVideo = false;
        }
    }

    private void LoadVideoClips()
    {
        // Existing Load logic here...
        // adding video clip to the array
        ads1[0].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/Cutscene1_Part1");
        ads1[1].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/AllTV");
        ads1[2].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/NoAndreTV");
        ads1[3].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/NoKieranTV");
        ads1[4].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/NoTylerTV");
        ads1[5].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/AndreSlapTV");
        ads1[6].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/TylerSlapTV");
        ads1[7].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/KieranSlapTV");
        ads1[8].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/AndreLoopTV");
        ads1[9].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/KieranLoopTV");
        ads1[10].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/TylerLoopTV");
        ads1[11].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/AndreFinalTV");
        ads1[12].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/TylerFinalTV");
        ads1[13].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/KieranFinalTV");
        ads1[14].videoClip = Resources.Load<VideoClip>("Video/Ads1Final/Cutscene1_Part2");
        //Debug.Log(ads1[0].videoClip);

        ads2[0].videoClip = Resources.Load<VideoClip>("Video/Ads2Testing/c2-pillow fort-plain");
        ads2[1].videoClip = Resources.Load<VideoClip>("Video/Ads2Testing/Ad2TV");
        // ... (rest of your loads)
    }
}