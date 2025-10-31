using Unity.Cinemachine;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public bool changeScene;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private CinemachineCamera cm;

    private void Start()
    {
        changeScene = false;
        cm.enabled = false;
    }
    private void Update()
    {
        if (changeScene)
        {
            cm.enabled = true;
            Invoke("changeSceneController", 5f);
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
