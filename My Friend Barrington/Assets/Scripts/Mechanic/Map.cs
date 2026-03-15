using UnityEngine;


public class Map : MonoBehaviour
{
    private Player player;
    public GameObject mapPanel;
    private bool isFrozen = true;
    void Start()
    {
        player = gameObject.findPlayer();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
            player.freezePlayer(isFrozen);
            isFrozen = !isFrozen;
        }


    }
}