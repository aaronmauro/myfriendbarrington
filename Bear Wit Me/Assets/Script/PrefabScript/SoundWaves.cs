using System.Collections;
using UnityEngine;

public class SoundWaves : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject soundWavesPrefab;
    private GameObject _soundWaveObject;
    [Header("SoundWaveControls")]
    [SerializeField]
    private float respawnSoundTime;
    [SerializeField]
    private float moveSpeed;
    private bool isSpawn;
    [SerializeField]
    private float destoryDistance;
    [Header("Direction")]
    [SerializeField]
    private bool isRight, isLeft, isUp, isDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawn)
        {
            StartCoroutine(spawnSoundWaves());
            isSpawn = true;
        }
        if (_soundWaveObject != null)
        {
            if (isRight)
            {
                _soundWaveObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else if (isLeft)
            {
                _soundWaveObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else if (isUp)
            {
                _soundWaveObject.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else if (isDown)
            {
                _soundWaveObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
        }
    }

    private IEnumerator spawnSoundWaves()
    {
        yield return new WaitForSeconds(respawnSoundTime);
        soundWaves();
    }

    private void soundWaves()
    {
        _soundWaveObject = Instantiate(soundWavesPrefab, transform.position, Quaternion.identity);
        if (_soundWaveObject == null)
        {
            isSpawn = false;
        }
    }
}
