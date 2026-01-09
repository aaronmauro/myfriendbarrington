using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

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
        // move camera
        if (!exitScreen)
        {
            cm.enabled = false;
        }
        else
        {
            return;
        }
    }

    // enable and disable skip level
    private void OnEnable()
    {
        InputManager.GetInstance().skipLevelAction.action.performed += skipLevel;
        InputManager.GetInstance().skipLevelAction.action.Enable();
    }
    private void OnDisable()
    {
        InputManager.GetInstance().skipLevelAction.action.performed -= skipLevel;
        InputManager.GetInstance().skipLevelAction.action.Disable();
    }

    private void Start()
    {
        if (exitScreen)
        {
            cm.enabled = false;
        }
        // Setting
        changeScene = false;
        vm = gameObject.findVideoManager();
        player = gameObject.findPlayer();
        // if cannot find the gameObject
        if (player == null) return;
        if (vm == null) return;
    }
    private void Update()
    {
        // Change Scene Script
        if (changeScene)
        {
            playVideo();
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
    // plays video before scene change
    private IEnumerator playVideo()
    {
        cm.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManagerScript.instance.nextScene(sceneName);
    }
    // skip level
    private void skipLevel(InputAction.CallbackContext context)
    {
        Debug.Log("why am i here");
        playVideo();
        StartCoroutine(playVideo());
        player.playerInput = false;
    }
}
