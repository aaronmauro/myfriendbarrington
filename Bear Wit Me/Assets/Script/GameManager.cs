using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the game manager
    // Respawn
    public bool backToSpawn;

    // Getting components
    [Header("components")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject[] spawnPoints;
    public bool addSpawnCount;
    public int spawnCount;

    void Start()
    {
        // getting components
        // seeting up booleans
        backToSpawn = false;
        addSpawnCount = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player trigger back to spawn
        if (backToSpawn)
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
