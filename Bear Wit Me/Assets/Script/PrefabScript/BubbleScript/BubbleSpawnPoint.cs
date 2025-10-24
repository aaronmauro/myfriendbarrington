using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawnPoint : MonoBehaviour
{
    // Inspector Bubble Controls
    [SerializeField]
    private float bubbleRespawnTime;
    private float bubbleRespawnTimer;

    // Bubble prefabs
    [SerializeField]
    private GameObject bubblesPrefab;
    private GameObject _spawnedBubblePrefab;

    [SerializeField]
    private float force;
    private int direction;

    private void Start()
    {
        direction = 2;
    }

    // Update is called once per frame
    void Update()
    {
        spawnBubble();
    }
    // Spawn Bubbles
    private void spawnBubble()
    {
        if (_spawnedBubblePrefab == null)
        {
            bubbleRespawnTimer += Time.deltaTime;
            if (bubbleRespawnTimer > bubbleRespawnTime)
            {
                _spawnedBubblePrefab = Instantiate(bubblesPrefab, transform.position, Quaternion.identity);
                bubbleRespawnTimer = 0;
            }
        }
    }
}
