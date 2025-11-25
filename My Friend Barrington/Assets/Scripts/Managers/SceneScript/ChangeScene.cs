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
    public bool exitScreen;

    private VideoManager vm;
    private Player player;
    private void Awake()
    {
        if (!exitScreen)
        {
            cm.enabled = false;
        }
        else
        {
            return;
        }
    }
    
    
    private void Start()
    {
        if (exitScreen)
        {
            cm.enabled = false;
        }
        // Setting
        changeScene = false;
        GameObject vmFind = GameObject.Find(GeneralGameTags.VideoManager);
        GameObject playerFind = GameObject.Find(GeneralGameTags.Player);
        
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
        // Change Scene Script
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
            //Invoke("changeSceneController", 5f);
            StartCoroutine(playVideo());
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
        if (other.gameObject.isPlayer())
        {
            playVideo();
            //Invoke("changeSceneController", 5f);
            StartCoroutine(playVideo());
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

    //public void buttonChangeScene(string name)
    //{
    //    SceneManagerScript.instance.nextScene(name);
    //}

    // plays video before scene change
    private IEnumerator playVideo()
    {
        cm.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManagerScript.instance.nextScene(sceneName);
    }

    //  disables cinemachine after video
    /*
    private IEnumerator exitVideo()
    {
        cm.enabled = true;
        yield return new WaitForSeconds(5f);
    }
    */
}
