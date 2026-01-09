using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private void Start()
    {
        // Make sure dialogue panel starts hidden
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // Hide continue button at start
        if (contButton != null)
        {
            contButton.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }
        }

        // Check if current dialogue is fully typed
        if (dialogueText != null && dialogue != null && index < dialogue.Length)
        {
            if (dialogueText.text == dialogue[index])
            {
                if (contButton != null)
                {
                    contButton.SetActive(true);
                }
            }
        }
    }

    public void zeroText()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
        index = 0;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (contButton != null)
        {
            contButton.SetActive(false);
        }
    } // <-- CLOSE zeroText() here!

    // Typing() should be its own separate method, not inside zeroText()
    IEnumerator Typing()
    {
        if (dialogue == null || index >= dialogue.Length || dialogueText == null)
        {
            yield break;
        }

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (contButton != null)
        {
            contButton.SetActive(false);
        }

        if (dialogue != null && index < dialogue.Length - 1)
        {
            index++;
            if (dialogueText != null)
            {
                dialogueText.text = "";
            }
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            SceneManager.LoadSceneAsync(8);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}