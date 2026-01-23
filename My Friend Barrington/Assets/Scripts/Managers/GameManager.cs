using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // This is the game manager

    // Getting components
    [Header("Components")]
    [SerializeField]
    private Player player;


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
            //AudioManager.instance.playBackgroundMusic(backGroundAudio);
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

    private void FixedUpdate() // THIS IS ALL TEMPORARY STUFF FOR PLAYTESTING WEEKLY BUILDS, BY DV
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SceneManager.LoadScene("Lvl 1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Lvl2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Lvl3");
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            player.transform.position = new Vector3(750,-195,0);
        }
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
        if (spawnPoints == null) return;
        if (player == null) return;

        //Debug.Log(player.moveRespawn);
        if (player.isGround && !dangerDetect)
        {
            spawnPoints.transform.position = player.transform.position;
        }
    }

}
