using Unity.Cinemachine;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public bool changeScene;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int nextAds;
    [SerializeField]
    private CinemachineCamera cm;

    private VideoManager vm;

    private void Awake()
    {
        cm.enabled = false;
    }
    private void Start()
    {
        changeScene = false;
        GameObject vmFind = GameObject.Find("VideoManager");
        if (vmFind != null)
        {
            vm = vmFind.GetComponent<VideoManager>();
        }
        //cm.enabled = false;
    }
    private void Update()
    {
        if (changeScene) 
        {
            cm.enabled = true;
            Invoke("changeSceneController", 5f);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("happy happy happy");
            cm.enabled = true;
            Invoke("changeSceneController", 5f);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cm.enabled = true;
            Invoke("changeSceneController", 5f);
        }
    }

    private void changeSceneController()
    {
        SceneManagerScript.instance.nextScene(sceneName);
        changeScene = false;
    }
}
