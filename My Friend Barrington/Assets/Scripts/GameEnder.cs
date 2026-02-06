using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    GameObject one;
    [SerializeField]
    GameObject two;
    [SerializeField]
    GameObject three;

    [SerializeField]
    GameObject end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!one.activeSelf && !two.activeSelf && !three.activeSelf)
        {
            end.SetActive(true);
        }
    }
}
