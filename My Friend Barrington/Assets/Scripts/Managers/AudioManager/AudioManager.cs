using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public AudioClass[] playerSound, bgSound, SFXSound, NPCSound;
    [SerializeField]
    private AudioSource playerSFX, bgSFX, SFX, NPCSFX;

    // create instance to access form other script
    public static AudioManager instance;

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

    public void playPlayerSFX(string name)
    {
        AudioClass _whatevereIWant = Array.Find(playerSound, x => x.names  == name);

        if (_whatevereIWant == null) 
        {
            Debug.Log("Audio name entered is wrong");
        }
        else
        {
            playerSFX.clip = _whatevereIWant.clip;
            playerSFX.Play();
        }
    }

    public void playBackgroundMusic(string name)
    {
        AudioClass _whatevereIWant = Array.Find(bgSound, x => x.names == name);

        if (_whatevereIWant == null)
        {
            Debug.Log("Audio name entered is wrong");
        }
        else
        {
            bgSFX.clip = _whatevereIWant.clip;
            bgSFX.Play();
        }
    }

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
