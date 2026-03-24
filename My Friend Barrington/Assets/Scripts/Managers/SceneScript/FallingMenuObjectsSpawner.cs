using UnityEngine;
using System.Collections;

public class FallingMenuObjectsSpawner : MonoBehaviour
{

    [SerializeField] float spawnTime;
    [SerializeField] GameObject[] menuThings;
    int item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item = Random.Range(0, menuThings.Length - 1);
        SpawnItem();
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnItem();
        StartCoroutine(SpawnTimer());
    }

    void SpawnItem()
    {
        item += Random.Range(1, menuThings.Length - 1);
        if (item >= 4) item -= 4;
        Instantiate(menuThings[item], new Vector2(Random.Range(0, Screen.width), Screen.height * 1.2f), Quaternion.Euler(0, 0, Random.Range(0, 359)), this.transform);
    }
}
