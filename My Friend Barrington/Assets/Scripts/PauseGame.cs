using Unity.VisualScripting;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PausePanel;

    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()

    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
