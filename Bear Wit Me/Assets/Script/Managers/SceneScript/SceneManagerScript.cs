using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Create instance to access in another script
    public static SceneManagerScript instance;

    // Don't destory on load
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Change scene method
    public void nextScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
