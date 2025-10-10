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
    private GameObject spawnedBubblePrefab;

    // Update is called once per frame
    void Update()
    {
        spawnBubble();
    }
    // Spawn Bubbles
    private void spawnBubble()
    {
        if (spawnedBubblePrefab == null)
        {
            bubbleRespawnTimer += Time.deltaTime;
            if (bubbleRespawnTimer > bubbleRespawnTime)
            {
                spawnedBubblePrefab = Instantiate(bubblesPrefab, transform.position, Quaternion.identity);
                bubbleRespawnTimer = 0;
            }
        }
    }
}
