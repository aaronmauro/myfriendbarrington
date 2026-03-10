using UnityEngine;
using System.Collections;

public class FallingMenuObjectsSpawner : MonoBehaviour
{

    [SerializeField] float spawnTime;
    [SerializeField] GameObject[] menuThings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        Debug.Log("why");
        SpawnItem();
        StartCoroutine(SpawnTimer());
    }

    void SpawnItem()
    {
        Debug.Log("plz");
        Instantiate(menuThings[0], this.transform);
        Debug.Log("you did it");
    }
}
