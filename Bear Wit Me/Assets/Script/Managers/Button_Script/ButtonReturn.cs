using UnityEngine;
using UnityEngine.UI;


public class ButtonReturn : MonoBehaviour
{
    // Getting video manager
    private VideoManager vm;
    ButtonManager bm;

    private void Start()
    {
        GameObject videoManager = GameObject.Find("VideoManager");
        vm = videoManager.GetComponent<VideoManager>();
        bm = FindObjectOfType<ButtonManager>();
        bm.buttons.Add(gameObject);
    }
    // When button are presssed
    public void onPress(int buttonValue)
    {
        vm.videoCount = buttonValue - 1;
        vm.afterLoopVideo = true;
        vm.loopVideo = false;
        bm.buttons.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
