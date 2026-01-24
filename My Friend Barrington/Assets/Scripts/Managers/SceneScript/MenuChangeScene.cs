using UnityEngine;
using UnityEngine.UI;

public class MenuChangeScene : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField] 
    private Button optionButton;
    [SerializeField]
    private Button mainButton;
    [SerializeField]
    private bool mainMenuCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Finding Button
        //startButton = GameObject.Find("StartButton").GetComponent<Button>();
        //optionButton = GameObject.Find("OptionsButton").GetComponent<Button>();
        //mainButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        if (mainMenuCheck)
        {
            startButton.onClick.AddListener(startGame);
            optionButton.onClick.AddListener(optionMenu);
        }
        else
        {
            mainButton.onClick.AddListener(mainMenu);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // start game
    private void startGame()
    {
        // going to level 1
        SceneManagerScript.instance.nextScene("whiteboxLevel1");
    }
    // option menu
    private void optionMenu()
    {
        // going to option menu
        SceneManagerScript.instance.nextScene("OptionsMenu");
    }
    // main menu
    private void mainMenu()
    {
        Debug.Log("Going to Main Menu");
        SceneManagerScript.instance.nextScene("MainMenu(StartScreen)");
    }
}
