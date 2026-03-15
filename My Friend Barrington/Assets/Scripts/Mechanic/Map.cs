using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Map UI")]
    public GameObject mapUI;

    [Header("Controls")]
    public KeyCode toggleKey = KeyCode.M;

    bool mapOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            mapOpen = !mapOpen;
            mapUI.SetActive(mapOpen);
        }
    }
}