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
    public bool backToSpawn;
    [SerializeField]
    private GameObject spawnPoints;
    [HideInInspector]
    public bool dangerDetect;

    public bool isInvincible;

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
    }

    void Start()
    {
        // getting components
        // seeting up booleans
        backToSpawn = false;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Respawn player
        respawn();
        
        // Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitTheGame();
        }
    }

    private void respawn()
    {
        // if player trigger back to spawn
        if (backToSpawn && !isInvincible)
        {
            player.transform.position = spawnPoints.transform.position;
            var playRg = player.GetComponent<Rigidbody>();
            playRg.linearVelocity = new Vector3(0, 0, 0);
            backToSpawn = false;
        }
        if (player.isGround && player.moveRespawn && !dangerDetect)
        {
            spawnPoints.transform.position = player.transform.position;
        }
    }

    private void exitTheGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
