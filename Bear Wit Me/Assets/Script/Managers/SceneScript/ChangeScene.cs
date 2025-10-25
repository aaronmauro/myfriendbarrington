using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public bool changeScene;
    [SerializeField]
    private string sceneName;

    private void Start()
    {
        changeScene = false;
    }
    private void Update()
    {
        if (changeScene)
        {
            SceneManagerScript.instance.nextScene(sceneName);
            changeScene = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManagerScript.instance.nextScene(sceneName);
        }
    }
}
