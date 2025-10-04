using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Getting Buttons
    [SerializeField]
    private Button[] buttons;
    // Getting Component
    [SerializeField]
    private VideoManager videoManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject vm = GameObject.Find("VideoManager");
        videoManager = vm.GetComponent<VideoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
