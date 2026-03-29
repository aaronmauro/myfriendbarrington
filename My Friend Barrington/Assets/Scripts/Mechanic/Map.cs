using UnityEngine;
using FMODUnity;

public class Map : MonoBehaviour
{
    [Header("Map UI")]
    public GameObject mapUI;

    [Header("Controls")]
    public KeyCode toggleKey = KeyCode.M;

     [Header("Audio (FMOD)")]
    [SerializeField] private EventReference mapOpenEvent;

    bool mapOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            RuntimeManager.PlayOneShotAttached(mapOpenEvent, gameObject);
            mapOpen = !mapOpen;
            mapUI.SetActive(mapOpen);
        }
    }
}