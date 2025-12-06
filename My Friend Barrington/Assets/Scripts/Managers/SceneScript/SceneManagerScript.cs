using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages scene changes
public class SceneManagerScript : MonoBehaviour
{
    // Create instance to access in another script
    public static SceneManagerScript instance;

    // Don't destory on load
    private void Awake()
    {
        // create singletons
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
    // wait second load scene
    public IEnumerator loadScene(string name)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
    // delay loading next scene methods
    public void delayNextScene(string name)
    {
        StartCoroutine(loadScene(name));
    }
}
