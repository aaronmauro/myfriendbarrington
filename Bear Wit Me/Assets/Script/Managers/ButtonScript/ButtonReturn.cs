using UnityEngine;
using UnityEngine.UI;


public class ButtonReturn : MonoBehaviour
{
    // Getting video manager
    private VideoManager vm;
    private ButtonManager bm;
    // Getting Input
    [SerializeField]
    private int inputValues;
    [SerializeField]
    private int adsButton;

    private void Start()
    {
        // Getting Compoenent
        GameObject videoManager = GameObject.Find("VideoManager");
        vm = videoManager.GetComponent<VideoManager>();
        bm = FindObjectOfType<ButtonManager>();
        bm.buttons[adsButton].Add(gameObject);
    }
    private void Update()
    {
        if (vm.videoCount > inputValues)
        {
            gameObject.SetActive(false);
            bm.buttons[adsButton].Remove(gameObject);
        }
    }
    // When button are presssed
    public void onPress(int buttonValue)
    {
        vm.videoCount = buttonValue - 1;
        vm.afterLoopVideo = true;
        vm.loopVideo = false;
        bm.buttonStatus = false;
        bm.buttons[adsButton].Remove(gameObject);
        gameObject.SetActive(false);
    }
}
