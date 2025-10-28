using System;
using UnityEngine;
using UnityEngine.Video;

// Output the script
[System.Serializable]
// Video class is not MonoBehaviour, I want it to be outputed
public class VideosData
{
    // set up name and Values we want to get in inspector
    public string Name;
    public VideoClip videoClip;
    public bool isLoop;
    public bool isSmashingButton;
}
