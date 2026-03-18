using UnityEngine;
using System.Collections;

public class FallingObjectDestroyer : MonoBehaviour
{

    [SerializeField] float destroyTime = 40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
