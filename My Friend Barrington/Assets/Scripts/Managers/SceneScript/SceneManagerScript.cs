using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages scene changes
public class SceneManagerScript : MonoBehaviour
{
    // Create instance to access in another script
    public static SceneManagerScript instance;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference creditSceneSound;
    [SerializeField] private EventReference mainMenuSceneSound;

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
        if (name == "CreditsMenu")
        {
            RuntimeManager.PlayOneShotAttached(creditSceneSound, gameObject);
        }
        else if (name == "MainMenu(StartScreen)")
        {
            RuntimeManager.PlayOneShotAttached(mainMenuSceneSound, gameObject);
            VideoManager.adsNumber = 0;
            VideoManager.newVideoCount = 0;
        }
    }
    public IEnumerator loadScene(string name)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
    public void delayNextScene(string name)
    {
        StartCoroutine(loadScene(name));
    }
}
