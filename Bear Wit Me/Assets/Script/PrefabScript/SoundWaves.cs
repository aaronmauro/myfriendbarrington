using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SoundWaves : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject soundWavesPrefab;
    private GameObject _soundWavesObject;
    private Collider _soundWavesCollider;
    private Collider playerCollider;
    private Player player;
    [Header("SoundWaveControls")]
    [SerializeField]
    private float respawnSoundTime;
    [SerializeField]
    private float moveSpeed;
    private bool isSpawn;
    [SerializeField]
    private float destoryDistance;
    [SerializeField]
    private float waveForce;

    [Header("Direction")]
    [SerializeField]
    private bool isRight, isLeft, isUp, isDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject findPlayer = GameObject.Find("Player");
        playerCollider = findPlayer.GetComponent<Collider>();
        player = findPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawn)
        {
            StartCoroutine(spawnSoundWaves());
            isSpawn = true;
        }
        if (_soundWavesObject != null)
        {
            if (isRight)
            {
                _soundWavesObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.x > soundWavesPrefab.transform.position.x + destoryDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
            else if (isLeft)
            {
                _soundWavesObject.transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.x < soundWavesPrefab.transform.position.x - destoryDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
            else if (isUp)
            {
                _soundWavesObject.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.y > soundWavesPrefab.transform.position.y + destoryDistance)
                {

                    Destroy(_soundWavesObject);
                }
            }
            else if (isDown)
            {
                _soundWavesObject.transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.y < soundWavesPrefab.transform.position.y - destoryDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
        }
        //Debug.Log(rbPlayer);
        if (playerCollider.bounds == null || _soundWavesCollider == null)
        {
            return;
        }
        else if (_soundWavesCollider.bounds.Intersects(playerCollider.bounds))
        {
            Debug.Log("sound wave touched");
            player.isPushed = true;
            if (isRight)
            {
                player.isPushedDirection(0,waveForce);
            }
            else if (isLeft)
            {
                player.isPushedDirection(1, waveForce);
            }
            else if (isUp)
            {
                player.isPushedDirection(2, waveForce);
            }
            else if (isDown)
            {
                player.isPushedDirection(3, waveForce);
            }
        }
        //it works just poping out so many error message, will figure it out
        //if (player != null)
        //{
        //    //Debug.Log("Player found");
        //}
    }

    private IEnumerator spawnSoundWaves()
    {
        yield return new WaitForSeconds(respawnSoundTime);
        soundWaves();
        isSpawn = false;
    }

    private void soundWaves()
    {
        _soundWavesObject = Instantiate(soundWavesPrefab, transform.position, Quaternion.identity);
        _soundWavesCollider = _soundWavesObject.GetComponent<Collider>();
        if (_soundWavesObject == null)
        {
            isSpawn = false;
        }
    }
}
