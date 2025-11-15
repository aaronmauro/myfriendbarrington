using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the game manager

    // Getting components
    [Header("Components")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private CinemachineCamera playerCam;

    // Manage respawn
    [Header("Respawn")]
    //public bool backToSpawn;
    [SerializeField]
    private GameObject spawnPoints;
    [HideInInspector]
    public bool dangerDetect;

    //public bool isInvincible;
    [Header("Music")]
    public string backGroundAudio;

    [Header("Build Components")]
    public int gameFrameRate;
    public bool vsync;

    private void Awake()
    {
        if (playerCam != null)
        {
            playerCam.enabled = true;
        }
        else
        {
            return;
        }

        // Application frame rate
        Application.targetFrameRate = gameFrameRate;
        QualitySettings.vSyncCount = vsync ? 1 : 0;

    }

    void Start()
    {
        // getting components
        // seeting up booleans
        //backToSpawn = false;
        //isInvincible = false;

        if (backGroundAudio == null)
        {
            return;
        }
        else
        {
            AudioManager.instance.playBackgroundMusic(backGroundAudio);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if player trigger back to spawn
        //if (backToSpawn && !isInvincible)
        //{
        //    // Respawn player
        //    respawn();
        //}
        
        moveRespawn();
        // Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitTheGame();
        }

        //Debug.Log(backToSpawn);
    }

    // respawn player Method
    public void respawn()
    {
        player.transform.position = spawnPoints.transform.position;
        var playRg = player.GetComponent<Rigidbody>();
        playRg.linearVelocity = Vector3.zero;
        //backToSpawn = false;
    }

    private void moveRespawn()
    {
        if (player.isGround && player.moveRespawn && !dangerDetect)
        {
            spawnPoints.transform.position = player.transform.position;
        }
    }

    // Method exit the game
    private void exitTheGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
