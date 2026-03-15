using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public GameObject PausePanel;

    public static bool GameIsPaused = false;


    public InputActionReference endAction;
    public GameObject resumeButton;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference resumeGameSound;
    [SerializeField] private EventReference quitGameSound;
    [SerializeField] private EventReference pauseGameSound;

    // Update is called once per frame

    private void Awake()
    {
        endAction.action.Enable();
        //endAction.action.performed += pressedPauseGame;
    }

    private void OnDisable()
    {
        //endAction.action.performed -= pressedPauseGame;
        endAction.action.Disable();
    }
    void Update()
    {
        if (endAction.action.triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    //private void FixedUpdate()
    //{
    //    if (GameIsPaused)
    //    {
    //        Pause();
    //    }
    //    else
    //    {
    //        Resume();
    //    }
    //}
    private void pressedPauseGame(InputAction.CallbackContext context)
    {
        //Debug.Log("not happy");
        GameIsPaused = true;
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()

    {
        RuntimeManager.PlayOneShotAttached(pauseGameSound, gameObject);
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void Continue()
    {
        RuntimeManager.PlayOneShotAttached(resumeGameSound, gameObject);
        PausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void LoadMenu(string nextSceneName)
    {
        VideoManager.adsNumber = 0;
        VideoManager.newVideoCount = 0;
        VideoManager.videoCount = 0;
        SceneManagerScript.instance.nextScene(nextSceneName);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        RuntimeManager.PlayOneShotAttached(quitGameSound, gameObject);
        Application.Quit();
        Debug.Log("quit");
    }
}
