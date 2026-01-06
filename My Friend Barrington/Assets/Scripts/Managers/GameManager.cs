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
        // interesting lambda, learn something new! quick and easy
        EndAction.action.Enable();
        EndAction.action.performed += ctx =>
        {
            Application.Quit();
            Debug.Log("Quit");
        };
    }
    private void OnDisable()
    {
        EndAction.action.Disable();
    }

    void Start()
    {
        // getting components
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
        // moving respawn point
        moveRespawn();
    }
    // respawn player Method
    public void respawn()
    {
        player.transform.position = spawnPoints.transform.position;
        var playRg = player.GetComponent<Rigidbody>();
        playRg.linearVelocity = Vector3.zero;
    }
    // move respawn method
    private void moveRespawn()
    {
        //Debug.Log(player.moveRespawn);
        if (player.isGround && !dangerDetect)
        {
            spawnPoints.transform.position = player.transform.position;
        }
    }
}
