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
        playerCam.enabled = true;
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
        // if player trigger back to spawn
        if (backToSpawn && !isInvincible)
        {
            player.transform.position = spawnPoints.transform.position;
            backToSpawn = false;
        }
        if (player.isGround && player.moveRespawn && !dangerDetect)
        {
            spawnPoints.transform.position = player.transform.position;
        }
        
    }
}
