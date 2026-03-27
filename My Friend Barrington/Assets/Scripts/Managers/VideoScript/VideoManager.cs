using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem;

public class VideoManager : MonoBehaviour
{
    // Inputing video clips
    [Header("Videos")]
    public VideosData[] ads1, ads2, ads3;
    private VideosData _v;
    public List<int> newVideoList = new List<int>(new int[8]);
    public static int newVideoCount;
    public static int adsNumber;

    [Header("Video Number")]
    [HideInInspector] public bool loopVideo;
    [HideInInspector] public static int videoCount;
    private int videoControlNumber;
    [HideInInspector] public bool afterLoopVideo;

    // Video status
    private long currentFrame;
    private long videoFrame;
    [HideInInspector] public bool playNextVideo;
    private bool isPauseVideo;
    //private bool fixingVideoLooping;

    // Change Scene
    public string[] nextSceneName;
    public KeyCode inputKey;
    public int skipVideoCount;

    [SerializeField] private GameObject remoteImage;
    [SerializeField] private GameObject texts;
    //private StudioEventEmitter fmodEventEmitter;

    [Header("Component")]
    [SerializeField] private VideoPlayer videoPlayer;
    private ButtonManager bm;
    //private FMOD.Studio.EventInstance videoAudioEvent;

    private void Awake()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.bgSFX.Stop();
        }

        InputManager.GetInstance().submitAction.action.performed += ads2ControllerPressed;
        InputManager.GetInstance().submitAction.action.Enable();
        // fmod - ensure the object name matches your hierarchy
        //GameObject fmodObj = GameObject.Find("FMODEvent");
        //if (fmodObj != null)
        //{
        //    fmodEventEmitter = fmodObj.GetComponent<StudioEventEmitter>();
        //}

        // Loading resources (Keep your existing paths)
        LoadVideoClips();
    }

    private void OnDisable()
    {
        InputManager.GetInstance().submitAction.action.performed -= ads2ControllerPressed;
        InputManager.GetInstance().submitAction.action.Disable();
    }
    void Start()
    {
        videoPlayer = GameObject.Find("VideoCanvas").GetComponent<VideoPlayer>();
        GameObject buttonManager = GameObject.Find(GeneralGameTags.ButtonManager);
        bm = buttonManager.GetComponent<ButtonManager>();

        newVideoList.Add(0);
        newVideoList.Add(1);

        playNextVideo = true;
        //fixingVideoLooping = false;
        videoCount = 0;
        newVideoCount = 0;
        videoControlNumber = 0;
        remoteImage.SetActive(false);
        texts.SetActive(false);

        //fmodEventEmitter.Play();

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
        else if (adsNumber == 2)
        {
            _v = Array.Find(ads3, x => x.Name == videoName);
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
            if (adsNumber == 1)
            {
                texts.SetActive(false);
            }

            // --- FMOD AUDIO SWAP ---
            /*            if (fmodEventEmitter != null)
                        {
                            *//* // Stop previous audio immediately
                             fmodEventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                             fmodEventEmitter.EventInstance.release();
                             // Update the event reference from your VideosData scriptable/struct
                             fmodEventEmitter.EventReference = _v.videoAudio;


                             // Play the new event
                             fmodEventEmitter.Play();*//*

                            playAudio(_v.videoAudio);
                        }*/

            // --- LOGIC CHECKS ---
            if (_v.isLoop)
            {
                loopVideo = true;
                bm.buttonStatus = true;
                remoteImage.SetActive(true);
                if (adsNumber == 1)
                {
                    texts.SetActive(true);
                }
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
            //fixingVideoLooping = true;
        //Debug.Log("testing");
        }

        //if (currentFrame < videoFrame - 1)
        //{
        //    fixingVideoLooping = false;
        //}
    }
    /*
        private void playAudio(EventReference newVideoAudio)
        {
            videoAudioEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            videoAudioEvent.release();
            videoAudioEvent = RuntimeManager.CreateInstance(newVideoAudio);
            var attributes = FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.gameObject);
            videoAudioEvent.set3DAttributes(attributes);
            videoAudioEvent.start();
        }*/
    public void ads2ControllerPressed(InputAction.CallbackContext context)
    {
        isPauseVideo = false;
        loopVideo = false;
    }
    private void LoadVideoClips()
    {
        // Existing Load logic here...
        // adding video clip to the array
        ads1[0].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/Cutscene1_Part1");
        ads1[1].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/AllTV");
        ads1[2].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/NoAndreTV");
        ads1[3].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/NoKieranTV");
        ads1[4].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/NoTylerTV");
        ads1[5].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/AndreSlapTV");
        ads1[6].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/TylerSlapTV");
        ads1[7].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/KieranSlapTV");
        ads1[8].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/AndreLoopTV");
        ads1[9].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/KieranLoopTV");
        ads1[10].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/TylerLoopTV");
        ads1[11].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/AndreFinalTV");
        ads1[12].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/TylerFinalTV");
        ads1[13].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/KieranFinalTV");
        ads1[14].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS1/Cutscene1_Part2");
        //Debug.Log(ads1[0].videoClip);

        ads2[0].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/c2-pillow fort-plain");
        ads2[1].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_0000");
        ads2[2].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_2 (Interactive)");
        ads2[3].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_3");
        ads2[4].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_4 (Interactive)");
        ads2[5].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_5");
        ads2[6].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_6 (Interactive)");
        ads2[7].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS2/KK_7");

        ads3[0].videoClip = Resources.Load<VideoClip>("Video/UnityVideoFolder/ADS3/CUTSCENE3_PLAYTEST");
        // ... (rest of your loads)
    }
}