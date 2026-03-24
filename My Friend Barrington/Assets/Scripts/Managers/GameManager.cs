using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using FMODUnity;


public class GameManager : MonoBehaviour
{
    // This is the game manager

    // Getting components
    [Header("Components")]
    [SerializeField]
    private Player player;
    private Animator anim;

    // Manage respawn
    [Header("Respawn")]
    //public bool backToSpawn;
    [SerializeField]
    private GameObject spawnPoints;
    [HideInInspector]
    public bool dangerDetect;

    [Header("Build Components")]
    public int gameFrameRate;
    public bool vsync;
    //public bool aspectRatio16_9;

    [Header("Interface")]
    public InputActionReference EndAction;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference quitGameSound;

    [Header("Aspect Ratio")]
    [SerializeField]
    private bool pc;
    private float targetaspect;
    private float windowaspect;
    private float scaleHeight;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera borderCamera;

    [Header("Teleport")]
    [SerializeField]
    Transform EndOfLevel;
    [SerializeField] Transform DebugTeleport;

    private void Awake()
    {
        // Application frame rate
        Application.targetFrameRate = gameFrameRate;
        QualitySettings.vSyncCount = vsync ? 1 : 0;

        borderCamera.depth = -2;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (pc)
        {
            targetaspect = 16.0f / 9.0f; // Aspect Ratio 16/9
        }
        else
        {
            targetaspect = 19.0f / 10.0f;
        }
            windowaspect = (float)Screen.width / Screen.height; // Window Size
        scaleHeight = windowaspect / targetaspect; // calculate current viewport
        
        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            mainCamera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleHeight;
            Rect rect = mainCamera.rect;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            mainCamera.rect = rect;
        }
    }

    private void OnEnable()
    {
        // interesting lambda, learn something new! quick and easy
/*        EndAction.action.Enable();
        EndAction.action.performed += ctx =>
        {
            Application.Quit();
            Debug.Log("Quit");
        };*/
    }
    private void OnDisable()
    {
        EndAction.action.Disable();
    }

    void Start()
    {
        // getting components
        player = gameObject.findPlayer();
        if (GameObject.Find("barrington") == null) return;
        anim = GameObject.Find("barrington").GetComponent<Animator>();
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
            player.transform.position = EndOfLevel.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.transform.position = DebugTeleport.position;
        }
    }

    // respawn player Method
    public void respawn()
    {
        //player.gameObject.SetActive(false); we got animation now
        if (player.playerInput)
        {
            Debug.Log("heavy is dead!!");
            player.freezePlayer(true);
            anim.SetTrigger("PlayerDeath");
            StartCoroutine(RespawnDelay1(1f));
        }

        
    }
    // move respawn method
    private void moveRespawn()
    {
        if (spawnPoints == null) return;
        if (player == null) return;
        if (!player.playerInput) return;
        if (player.isGround && !dangerDetect)
        {
            
            spawnPoints.transform.position = player.transform.position;
        }
    }

    private IEnumerator RespawnDelay1(float delay) // they aint ready for this one - DV
    {
        
        yield return new WaitForSeconds(delay);

        
        //player.transform.position = spawnPoints.transform.position;
        player.TeleportTo(spawnPoints.transform);
        //var playRg = player.GetComponent<Rigidbody>();
        //playRg.linearVelocity = Vector3.zero;
        StartCoroutine(RespawnDelay2(1f));
    }

    private IEnumerator RespawnDelay2(float delay) // ohhhh they aint ready - DV
    {
        yield return new WaitForSeconds(delay);
        //player.gameObject.SetActive(true);
        
        player.freezePlayer(false);
    }
    
    // quit the game for escape button
    public void onEscapePressed()
    {
        RuntimeManager.PlayOneShotAttached(quitGameSound, gameObject);
        Debug.Log("Quit");
        Application.Quit();
    }
}
