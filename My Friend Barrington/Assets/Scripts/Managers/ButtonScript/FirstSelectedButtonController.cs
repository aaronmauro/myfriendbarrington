using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FirstSelectedButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject firstSelected;
    [SerializeField]
    private GameObject secondSelected;
    [SerializeField]
    private GameObject thirdSelected;
    public static bool isController = true;


    void Start()
    {
        if (!isController) return;
        if (EventSystem.current == null) return;

        // Use same logic as changeSelectedButton to pick the appropriate initial selection
        changeSelectedButton();
    }

    // Update is called once per frame
    void Update()
    {
        changeSelectedButton();
    }

    private void changeSelectedButton()
    {
        // if not using controller or no EventSystem, do nothing
        if (!isController || EventSystem.current == null) return;

        // Priority: thirdSelected -> secondSelected -> firstSelected
        if (thirdSelected != null && thirdSelected.activeInHierarchy)
        {
            EventSystem.current.firstSelectedGameObject = thirdSelected;
        }
        else if (secondSelected != null && secondSelected.activeInHierarchy)
        {
            EventSystem.current.firstSelectedGameObject = secondSelected;
        }
        else
        {
            EventSystem.current.firstSelectedGameObject = firstSelected;
        }
    }
}
