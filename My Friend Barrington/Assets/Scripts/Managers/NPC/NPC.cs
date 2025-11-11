using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;




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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (DialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(DialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }


    }

    public void zeroText()
    {
        DialogueText.text = "";
        index = 0;
        DialoguePanel.SetActive(false);
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
