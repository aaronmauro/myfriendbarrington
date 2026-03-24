using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracker : MonoBehaviour
{

    public Image original;
    public Sprite newSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision Info)
    {
        if(Info.gameObject.tag == "BubbleBottle")
        { 
            Debug.Log("You've knocked the bottle!");
            original.sprite = newSprite;    

        }
           

    }
}
