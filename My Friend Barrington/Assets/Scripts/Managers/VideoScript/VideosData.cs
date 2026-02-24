using System;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;

[System.Serializable]
public class VideosData
{
    [Header("Identity")]
    public string Name;

    [Header("Media Assets")]
    public VideoClip videoClip;
    public EventReference videoAudio; // This allows you to pick the FMOD event from the UI

    [Header("Settings")]
    public bool isLoop;
    public bool isSmashingButton;
}