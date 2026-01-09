using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;


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
    public static bool playerIsClose;
    public static bool inDialouge;

    public InputActionReference talkAction;
    //public InputActionReference continueAction;

    void Update()
    {
        if (!NPC.playerIsClose)
        {
            zeroText();
            return;
        }

        /*
        if (talkAction.action.triggered && !NPC.inDialouge)
        {
            if (DialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                // Play npc audio
                AudioManager.instance.playNPCSFX("FelliniFerret");
                DialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                NPC.inDialouge = true;
            }
        }
        */
        if (!talkAction.action.triggered)
        {
            if (DialogueText.text == dialogue[index])
            {
                //Debug.Log("Have fun");
                continueButton.SetActive(true);
            }
            return;
        }
        if (NPC.inDialouge)
        {
            NextLine();
            // Play npc audio
            AudioManager.instance.playNPCSFX("FelliniFerret");
            return;
        }
        if (DialoguePanel.activeInHierarchy)
        {
            zeroText();
            return;
        }
        /*
        if (DialogueText.text == dialogue[index])
        {
            //Debug.Log("Have fun");
            continueButton.SetActive(true);

            //// Check controller input for advancing dialogue
            //if (talkAction.action.triggered)
            //{

            //}
        }
        */
        // Play npc audio
        AudioManager.instance.playNPCSFX("FelliniFerret");
        DialoguePanel.SetActive(true);
        StartCoroutine(Typing());
        NPC.inDialouge = true;
    }
        /*
        else
        {
            zeroText();
        }
        */
        //Debug.Log(dialogue.Length);

    

    public void zeroText()
    {
        DialogueText.text = "";
        index = 0;
        DialoguePanel.SetActive(false);
        NPC.inDialouge = false;
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
        if (other.CompareTag(GeneralGameTags.Player))
        {
            
            NPC.playerIsClose = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GeneralGameTags.Player))
        {
            NPC.playerIsClose = false;
            interactPrompt.SetActive(false);
            zeroText();
        }
    }
}
