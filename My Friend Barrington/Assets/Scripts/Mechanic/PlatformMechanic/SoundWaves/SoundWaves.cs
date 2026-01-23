using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SoundWaves : MonoBehaviour
{
    // Storing Prefabs
    [Header("Prefab")]
    [SerializeField]
    private GameObject soundWavesPrefab;
    private GameObject _soundWavesObject;
    private Collider _soundWavesCollider;
    private Collider playerCollider;
    private Player player;
    private GameObject findPlayer;
    // Sound wave status
    [Header("SoundWaveControls")]
    [SerializeField]
    private float respawnSoundTime;
    [SerializeField]
    private float moveSpeed;
    private bool isSpawn;
    [SerializeField]
    private float destroyDistance;
    private float travelDistance;
    [SerializeField]
    private float waveForce;
    public bool inBush;

    // Trigger to Activate
    [Header("RunCode")]
    public bool isRun;
    
    // enumerations of different directions
    public enum GoingDirection
    {
        isRight, isLeft, isUp, isDown
    }
    // Getting Direction
    [Header("Direction")]
    public GoingDirection goingdirection;

    private Vector3 direction = Vector3.zero;
    //[SerializeField]
    //private bool isRight, isLeft, isUp, isDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Getting Compoennt
        findPlayer = GameObject.Find(GeneralGameTags.Player);
        playerCollider = findPlayer.GetComponent<Collider>();
        player = findPlayer.GetComponent<Player>();
        inBush = false;

        direction = checkingDirection(goingdirection);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // return if don't run this script
        if (!isRun) return;

        // If sound Wave is not Spawned
        if (!isSpawn)
        {
            StartCoroutine(spawnSoundWaves());
            isSpawn = true;
        }
        //Debug.Log(inBush);
        // If sound Wave is Spawned
        if (_soundWavesObject != null)
        {
            moveSoundWaves();
            //Debug.Log(direction);
        }
        pushPlayer();
        //it works just poping out so many error message, will figure it out
        //if (player != null)
        //{
        //    //Debug.Log("Player found");
        //}
    }

    // Respawn Sound Waves after certain time
    private IEnumerator spawnSoundWaves()
    {
        yield return new WaitForSeconds(respawnSoundTime);
        soundWaves();
        isSpawn = false;
    }

    // Generate Sound Waves
    private void soundWaves()
    {
       
        _soundWavesObject = Instantiate(soundWavesPrefab, transform.position, Quaternion.identity);
        _soundWavesCollider = _soundWavesObject.GetComponent<Collider>();
        if (_soundWavesObject == null)
        {
            isSpawn = false;
        }
    }

    // checking soundwave moving direction
    private Vector3 checkingDirection(GoingDirection dir)
    {

            /*
            // Check direction
            if (isRight)
            {
                // Moving Sound Waves
                _soundWavesObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.x > transform.position.x + destroyDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
            else if (isLeft)
            {
                _soundWavesObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.x < transform.position.x - destroyDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
            // Up and Down
            else if (isUp)
            {
                _soundWavesObject.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.y > transform.position.y + destroyDistance)
                {

                    Destroy(_soundWavesObject);
                }
            }
            // Down
            else if (isDown)
            {
                _soundWavesObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                if (_soundWavesObject.transform.position.y < transform.position.y - destroyDistance)
                {
                    Destroy(_soundWavesObject);
                }
            }
            */
            switch (dir)
            {
                case GoingDirection.isRight:
                    return Vector3.right;
                case GoingDirection.isLeft:
                    return Vector3.left;
                case GoingDirection.isUp:
                    return Vector3.up;
                case GoingDirection.isDown:
                    return Vector3.down;
                default:
                    return Vector3.zero;
            }
        }

    // moving sound wave and destory when reaching a destory distance
    private void moveSoundWaves()
    {
        //_soundWavesObject.transform.Translate(direction * moveSpeed * Time.deltaTime);
        _soundWavesObject.GetComponent<Rigidbody>().MovePosition(_soundWavesObject.transform.position+direction*moveSpeed*Time.fixedDeltaTime);
        travelDistance = Vector3.Distance(_soundWavesObject.transform.position, transform.position);
        if (travelDistance > destroyDistance)
        {
            Destroy(_soundWavesObject);
        }
    }

    // Pushing player method
    private void pushPlayer()
    {
        //Debug.Log(rbPlayer);
        // If no player or sound wave don't do anything
        if (playerCollider.bounds == null || _soundWavesCollider == null)
        {
            return;
        }
        // If sound wave collide with player
        else if (_soundWavesCollider.bounds.Intersects(playerCollider.bounds) && !inBush)
        {
            //Debug.Log("sound wave touched");
            //player.isPushed = true;
            Physics.IgnoreCollision(playerCollider, _soundWavesCollider, false);
            //if (isRight)
            //{
            player.pushingPlayer(direction, waveForce);
            //}
            /*
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
            */
        }
        else if (inBush)
        {
            Physics.IgnoreCollision(playerCollider, _soundWavesCollider, true);
        }
    }
}
