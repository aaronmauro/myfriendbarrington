using System;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class InkNPC : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    void Awake()
    {
        RemoveChildren();
        ReadStory();

    }

    void ReadStory()
    {
        story = new Story(inkJSONAsset.text);
            if (OnCreateStory != null) OnCreateStory(story);
        DisplayAllContent();
    }

    void DisplayAllContent()
    {
        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                CreateContentView(text);
            }
            
        }
        DisplayChoices();
    }

    void DisplayChoices()
    {
        int actualChoiceCount = story.currentChoices.Count;


        for (int i = 0; i < actualChoiceCount && i < 4; i++)
        {
            Choice choice = story.currentChoices[i];
            Button button = CreateChoiceView(choice.text.Trim());

            button .onClick.AddListener(delegate
            {
                OnClickChoiceButton(choice);
            });



        }

        for (int i = actualChoiceCount; i < 4; i++)
        {
            Button emptyButton = CreateChoiceView("");
            emptyButton.interactable = false;
        }
        
    }
        
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        DisplayAllContent();

    }


