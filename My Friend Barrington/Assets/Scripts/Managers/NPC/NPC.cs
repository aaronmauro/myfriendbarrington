using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;


// sorry to touch your code (Eric)
public class NPC : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject DialoguePanel;
    public Text DialogueText;
    public string[] dialogue;
    private int index;

    public GameObject  continueButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool inDialouge;

    public InputActionReference talkAction;
    //public InputActionReference continueAction;

    void Update()
    {
        if (playerIsClose)
        {
            if (talkAction.action.triggered && !inDialouge)
            {
                if (DialoguePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    DialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                    inDialouge = true;
                }


            }
            else if (talkAction.action.triggered && inDialouge)
            {
                NextLine();

            }
            if (DialogueText.text == dialogue[index])
            {
                //Debug.Log("Have fun");
                continueButton.SetActive(true);

                //// Check controller input for advancing dialogue
                //if (talkAction.action.triggered)
                //{

                //}
            }

        }
        else
        {
            zeroText();
        }
        //Debug.Log(dialogue.Length);

    }

    public void zeroText()
    {
        DialogueText.text = "";
        index = 0;
        DialoguePanel.SetActive(false);
        inDialouge = false;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        continueButton.SetActive(false);    

        if (index < dialogue.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerIsClose = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            interactPrompt.SetActive(false);
            zeroText();
        }
    }
}
