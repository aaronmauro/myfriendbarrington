using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject firstSelected;
    [SerializeField]
    private GameObject secondSelected;
    public static bool isController = true;

    void Start()
    {
        if (isController)
        {
            EventSystem.current.firstSelectedGameObject = firstSelected;
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeSelectedButton();
    }

    private void changeSelectedButton()
    {
        // if controller
        if (!isController) return;
        if (secondSelected == null) return;

        if (secondSelected.activeInHierarchy)
        {
            EventSystem.current.firstSelectedGameObject = secondSelected;
        }
        else
        {
            EventSystem.current.firstSelectedGameObject = firstSelected;
        }
    }
}
