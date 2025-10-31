using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    int index = 2;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount> 1)
        {
            if (Player.dialogue)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                index += 1;
                if(transform.childCount == index)
                {
                    index = 2;
                    Player.dialogue = false;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }
}
