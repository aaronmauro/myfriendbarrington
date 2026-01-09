using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // lots of different sounds
    public AudioClass[] playerSound, bgSound, SFXSound, NPCSound;
    [SerializeField]
    private AudioSource playerSFX, bgSFX, SFX, NPCSFX, playerWalkingSFX;

    // create instance to access form other script
    public static AudioManager instance;

    // singletons this
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
    }
    // find audio in player sfx list and play it in player sfx mixer
    public void playPlayerSFX(string name)
    {
        // find the audio by name(string) in the list
        AudioClass _whatevereIWant = Array.Find(playerSound, x => x.names  == name);

        // if didn't find the audio
        if (_whatevereIWant == null) 
        {
            Debug.Log("Audio name entered is wrong");
        }
        // play the audio
        else
        {
            playerSFX.clip = _whatevereIWant.clip;
            playerSFX.enabled = true;
            playerSFX.Play();
        }
    }
    // find audio in player walking sfx list and play it in player walking sfx mixer
    public void playPlayerWalking(bool isWalking)
    {
        if (isWalking)
        {
            playerWalkingSFX.enabled = true;
        }
        else
        {
            playerWalkingSFX.enabled = false;
        }
    }
    // find audio in background music list and play it in background music mixer
    public void playBackgroundMusic(string name)
    {
        AudioClass _whatevereIWant = Array.Find(bgSound, x => x.names == name);

        if (_whatevereIWant == null)
        {
            Debug.Log("Audio name entered is wrong");
            //Debug.Log(videoScene);
            bgSFX.Stop();
        }
        else
        {
            bgSFX.clip = _whatevereIWant.clip;
            bgSFX.Play();
        }
    }
    // find audio in general sfx list and play it in general sfx mixer
    public void playSFX(string name)
    {
        AudioClass _whatevereIWant = Array.Find(SFXSound, x => x.names == name);

        if (_whatevereIWant == null)
        {
            Debug.Log("Audio name entered is wrong");
        }
        else
        {
            SFX.clip = _whatevereIWant.clip;
            SFX.Play();
        }
    }
    // find audio in npc sfx list and play it in npc sfx mixer
    public void playNPCSFX(string name)
    {
        AudioClass _whatevereIWant = Array.Find(NPCSound, x => x.names == name);

        if (_whatevereIWant == null)
        {
            Debug.Log("Audio name entered is wrong");
        }
        else
        {
            NPCSFX.clip = _whatevereIWant.clip;
            NPCSFX.Play();
        }
    }
}
