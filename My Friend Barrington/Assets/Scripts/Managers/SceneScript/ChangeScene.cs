using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // changes scene
    public bool changeScene;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int nextAds;
    [SerializeField]
    private CinemachineCamera cm;

    private VideoManager vm;
    private Player player;
    private void Awake()
    {
        cm.enabled = false;
    }
    
    
    private void Start()
    {
        
        changeScene = false;
        GameObject vmFind = GameObject.Find("VideoManager");
        GameObject playerFind = GameObject.Find("Player");
        
        if (vmFind != null)
        {
            vm = vmFind.GetComponent<VideoManager>();
        }
        if (playerFind != null)
        {
            player = playerFind.GetComponent<Player>();
        }
        //cm.enabled = false;
    }
    private void Update()
    {
        //Debug.Log(player.playerInput);
        if (changeScene)
        {
            playVideo();
            //Invoke("changeSceneController", 5f);
            player.playerInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("happy happy happy");
            playVideo();
            Invoke("changeSceneController", 5f);
            player.playerInput = false;
        }
        if (vm == null)
        {
            return;
        }
        else
        {
            VideoManager.adsNumber = nextAds;
        }
    }
    
    // triggers scene change when player enters collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playVideo();
            Invoke("changeSceneController", 5f);
            player.playerInput = false;
        }
    }

    // controls scene change
    private void changeSceneController()
    {
        player.playerInput = true;
        SceneManagerScript.instance.nextScene(sceneName);
        changeScene = false;
    }

    // plays video before scene change
    private IEnumerator playVideo()
    {
        cm.enabled = true;
        yield return new WaitForSeconds(5f);
        CinemachineFollow testing = cm.GetComponent<CinemachineFollow>();
    }

    //  disables cinemachine after video
    private IEnumerator exitVideo()
    {
        cm.enabled = true;
        yield return new WaitForSeconds(5f);
    }
}
