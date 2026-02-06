using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor.U2D;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] public TextMeshProUGUI dialogueName;
    [SerializeField] public Image dialogueImage;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Continue Button")]
    [SerializeField] private GameObject continueButton;

    [Header("Typewriter Effect")]
    [SerializeField] private float typingSpeed = 0.04f; // Time between each character
    [SerializeField] private bool canSkipTyping = true; // Allow players to skip typing animation

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private Coroutine selectFirstChoiceCoroutine;
    private Coroutine typingCoroutine;

    private bool isProcessingChoice = false;
    private bool isTyping = false;
    private string currentText = "";

    private static DialogueManager instance;
    public event System.Action OnDialogueEnd;
    [SerializeField] private Player player;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            // Get ALL TextMeshProUGUI components and find the one that's a child of the button
            TextMeshProUGUI[] textComponents = choice.GetComponentsInChildren<TextMeshProUGUI>();

            // If there are multiple text components, use the last one (usually the one inside the button)
            if (textComponents.Length > 0)
            {
                choicesText[index] = textComponents[textComponents.Length - 1];
            }
            else
            {
                Debug.LogError($"No TextMeshProUGUI found on choice button {index}!");
            }

            index++;
        }

        // Set up continue button
        if (continueButton != null)
        {
            continueButton.SetActive(false);
            Button btn = continueButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnContinueButtonClicked);
            }
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        // Allow Space to skip typing or continue when there are no choices
        if (Input.GetKeyDown(KeyCode.Space) && currentStory.currentChoices.Count == 0)
        {
            if (isTyping && canSkipTyping)
            {
                // Skip the typing animation
                SkipTyping();
            }
            else if (!isTyping)
            {
                // Continue to next line
                ContinueStory();
            }
        }
    }

    // Called when continue button is clicked
    public void OnContinueButtonClicked()
    {
        Debug.Log("Continue button clicked");

        if (currentStory == null)
        {
            Debug.LogError("Current story is null!");
            return;
        }

        // If currently typing, skip to the end
        if (isTyping && canSkipTyping)
        {
            SkipTyping();
            return;
        }

        if (currentStory.canContinue)
        {
            // Continue to next line
            ContinueStory();
        }
        else
        {
            // End dialogue
            StartCoroutine(ExitDialogueMode());
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Debug.Log("=== EnterDialogueMode called ===");
        Debug.Log("inkJSON: " + (inkJSON != null ? inkJSON.name : "NULL"));

        currentStory = new Story(inkJSON.text);
        Debug.Log("Story created. Can continue: " + currentStory.canContinue);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        Debug.Log("Panel activated");

        ContinueStory();
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        if (currentStory != null &&
            currentStory.currentChoices.Count > 0 &&
            choices != null &&
            choices.Length > 0 &&
            choices[0] != null &&
            choices[0].activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        // Stop any ongoing typing
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        OnDialogueEnd?.Invoke();
    }

    private void ContinueStory()
    {
        Debug.Log("=== ContinueStory called ===");
        Debug.Log("Can continue: " + currentStory.canContinue);

        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();
            Debug.Log("Story text: " + text);

            // Start typing effect instead of displaying text immediately
            currentText = text;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(text));

            Debug.Log("Current choices count: " + currentStory.currentChoices.Count);
        }
        else
        {
            Debug.Log("Story cannot continue - exiting");
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        // Hide choices and continue button while typing
        HideAllChoices();
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        // After typing is complete, display choices or continue button
        DisplayChoices();
    }

    private void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = currentText;
        isTyping = false;

        // Display choices or continue button after skipping
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // If there are choices, show them and hide continue button
        if (currentChoices.Count > 0)
        {
            if (continueButton != null)
            {
                continueButton.SetActive(false);
            }

            if (currentChoices.Count > choices.Length)
            {
                Debug.LogError("More choices than UI supports: " + currentChoices.Count);
            }

            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                if (index >= choices.Length)
                    break;

                if (choices[index] != null)
                {
                    choices[index].gameObject.SetActive(true);

                    if (choicesText[index] != null)
                    {
                        choicesText[index].text = choice.text;
                    }

                    int choiceIndex = index;
                    Button button = choices[index].GetComponent<Button>();

                    if (button != null)
                    {
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => MakeChoice(choiceIndex));
                        button.interactable = true;
                    }
                }

                index++;
            }

            // Hide unused choice buttons
            for (int i = index; i < choices.Length; i++)
            {
                if (choices[i] != null)
                {
                    choices[i].gameObject.SetActive(false);
                }
            }

            if (selectFirstChoiceCoroutine != null)
            {
                StopCoroutine(selectFirstChoiceCoroutine);
            }

            selectFirstChoiceCoroutine = StartCoroutine(SelectFirstChoice());
        }
        else
        {
            // No choices - hide all choice buttons
            HideAllChoices();

            if (currentStory.canContinue)
            {
                // More dialogue - show continue button
                if (continueButton != null)
                {
                    continueButton.SetActive(true);
                    TextMeshProUGUI btnText = continueButton.GetComponentInChildren<TextMeshProUGUI>();

                    if (btnText != null)
                    {
                        btnText.text = "Continue";
                    }
                }
            }
            else
            {
                // Story ended - show close button
                if (continueButton != null)
                {
                    continueButton.SetActive(true);
                    TextMeshProUGUI btnText = continueButton.GetComponentInChildren<TextMeshProUGUI>();

                    if (btnText != null)
                    {
                        btnText.text = "Close";
                    }
                }
            }
        }
    }

    private void HideAllChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (isProcessingChoice)
        {
            Debug.Log("Already processing a choice, ignoring");
            return;
        }

        Debug.Log($"MakeChoice called with index: {choiceIndex}");
        Debug.Log($"Available choices BEFORE: {currentStory.currentChoices.Count}");

        if (currentStory == null)
        {
            Debug.LogError("Cannot make choice - currentStory is null!");
            return;
        }

        if (choiceIndex < 0 || choiceIndex >= currentStory.currentChoices.Count)
        {
            Debug.LogError($"Choice index {choiceIndex} is out of range! Available: {currentStory.currentChoices.Count}");
            return;
        }

        // IMMEDIATELY disable all choice buttons to prevent double-clicks
        isProcessingChoice = true;
        foreach (GameObject choice in choices)
        {
            if (choice != null)
            {
                Button btn = choice.GetComponent<Button>();
                if (btn != null)
                {
                    btn.interactable = false;
                }
            }
        }

        // Now process the choice
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();

        // Re-enable buttons for next set of choices (DisplayChoices will handle this)
        isProcessingChoice = false;
    }

    public void UpdateNpc(string npcName, Sprite npcImage)
    {
        dialogueName.SetText(npcName);
        dialogueImage.sprite = npcImage;
    }
}