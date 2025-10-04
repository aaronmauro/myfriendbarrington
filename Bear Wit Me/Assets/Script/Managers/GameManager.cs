using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the game manager

    // Getting components
    [Header("Components")]
    [SerializeField]
    private Player player;

    // Manage respawn
    [Header("Respawn")]
    public bool backToSpawn;
    [SerializeField]
    private GameObject[] spawnPoints;
    public bool addSpawnCount;
    public int spawnCount;

    public bool isInvincible;

    void Start()
    {
        // getting components
        // seeting up booleans
        backToSpawn = false;
        addSpawnCount = false;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player trigger back to spawn
        if (backToSpawn && !isInvincible)
        {
            player.transform.position = spawnPoints[spawnCount].transform.position;
            backToSpawn = false;
        }
        if (addSpawnCount)
        {
            spawnCount++;
            addSpawnCount = false;
            /*
            if (spawnCount > spawnPoints.Length)
            {
                spawnCount = 0;
            }
            */ // To be decided
        }
    }
}
