using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    // This is the game manager

    // Getting components
    [Header("Components")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private CinemachineCamera playerCam;

    private bool isVideoScene;

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

    public InputActionReference EndAction;

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

    private void OnEnable()
    {
        EndAction.action.Enable();
        EndAction.action.performed += exitTheGame;
    }

    void Start()
    {
        // getting components
        // seeting up booleans
        //backToSpawn = false;
        //isInvincible = false;

        //Debug.Log(backGroundAudio);
        if (backGroundAudio == "") // Why can't this bull null :<
        {
            AudioManager.instance.playBackgroundMusic(backGroundAudio);
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
        //if (EndAction.action.triggered)
        //{
        //    exitTheGame();
        //}

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
    public void exitTheGame(InputAction.CallbackContext context)
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
