using UnityEngine;
using UnityEngine.UI;


public class ButtonReturn : MonoBehaviour
{
    // Getting video manager
    private VideoManager vm;
    private ButtonManager bm;
    private Button button;
    // Getting Input
    [SerializeField]
    private int[] inputValues;
    [SerializeField]
    public int buttonValue;
    [SerializeField]
    private int adsButton;

    private void Awake()
    {
        // finding button manager
        bm = GameObject.Find(GeneralGameTags.ButtonManager).GetComponent<ButtonManager>();
        button = gameObject.GetComponent<Button>();
    }
    private void Start()
    {
        // Getting Compoenent
        GameObject videoManager = GameObject.Find(GeneralGameTags.VideoManager);
        vm = videoManager.GetComponent<VideoManager>();

        // Expand the button list
        if (VideoManager.adsNumber == adsButton )
        {
            bm.buttons[adsButton].Add(gameObject);
        }

        // Listener for on button pressed
        button.onClick.AddListener(playerOnClick);
    }
    private void Update()
    {
        // checking what video to show the buttons
        //if (vm.videoCount > inputValues[bm.numberOfChocie])
        //{
        //    gameObject.SetActive(false);
        //    bm.buttons[adsButton].Remove(gameObject);
        //}
        //else if (VideoManager.adsNumber > adsButton)
        //{
        //    gameObject.SetActive(false);
        //}
        if (VideoManager.adsNumber > adsButton)
        {
            gameObject.SetActive(false);
        }
    }
    // When button are presssed - this is not using for now
    public void onPress(int buttonValue)
    {
        vm.videoCount = buttonValue - 1;
        vm.afterLoopVideo = true;
        vm.loopVideo = false;
        bm.buttonStatus = false;
        bm.buttons[adsButton].Remove(gameObject);
        gameObject.SetActive(false);
    }
    // checking when player click
    private void playerOnClick()
    {
        Debug.Log("testing");
        vm.videoCount = inputValues[bm.numberOfChocie]; // testing
        vm.newVideoList.Add(inputValues[bm.numberOfChocie]);
        vm.afterLoopVideo = true;
        bm.numberOfChocie++;
        vm.loopVideo = false;
        bm.buttonStatus = false;
        bm.buttons[adsButton].Remove(gameObject);
        gameObject.SetActive(false);
        bm.calculateVideoNumber();
    }

    // all the status when button the pressed - delay
    public void onButtonPressed(ButtonManager bm)
    {
        vm.videoCount = inputValues[bm.numberOfChocie] - 1;
        vm.afterLoopVideo = true;
        vm.loopVideo = false;
        bm.buttonStatus = false;
        bm.buttons[adsButton].Remove(gameObject);
        gameObject.SetActive(false);
    }
}
