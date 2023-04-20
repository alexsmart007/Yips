using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueHelperClass;

public class DialogueUIController : SerializedMonoBehaviour
{
    [SerializeField] PortraitDisplay leftPortrait, rightPortrait;
    [SerializeField] TextBoxDisplay textBoxDisplay;
    [SerializeField] ChoicesDisplay choicesDisplay;
    [SerializeField] Image background;
    [SerializeField] new UIAudio audio;

    List<string> choices = new List<string>();
    int currentChoice;
    int totalChoices;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += DisplayUI;
        DialogueManager.OnDialogueEnded += HideUI;
        DialogueManager.OnChoiceMenuOpen += OnChoiceMenuStart;
        DialogueManager.OnChoiceMenuClose += OnChoiceMenuEnd;
        HideUI();
    }

    private void HideUI()
    {
        background.gameObject.SetActive(false);
        leftPortrait.Hide();
        rightPortrait.Hide();
        textBoxDisplay.Hide();
        choicesDisplay.Hide();
        DialogueManager.OnTextUpdated -= textBoxDisplay.UpdateDialogueText;
    }

    private void DisplayUI(ConversationData conversation)
    {
        background.gameObject.SetActive(true);
        leftPortrait.Display(conversation.Conversant);
        rightPortrait.Display("detective birch");
        textBoxDisplay.Display();
        DialogueManager.OnTextUpdated += textBoxDisplay.UpdateDialogueText;
    }

    private void UpdateChoiceSelection(Vector2 navigation)
    {
        if (audio != null) audio.PlaySwap();

        currentChoice += Mathf.RoundToInt(navigation.normalized.x);
        currentChoice = Mathf.Clamp(currentChoice, 0, totalChoices - 1);
        choicesDisplay.SelectChoice(currentChoice);
    }

    private void OnChoiceClicked(int index)
    {
        if (index == currentChoice)
        {

            SelectChoice();
        }
        else
        {
            if (audio != null) audio.PlaySwap();
            currentChoice = index;
            choicesDisplay.SelectChoice(index);
        }
    }

    private void SelectChoice()
    {
        if (audio != null) audio.PlayClick();
        Debug.Log("Selecting: " + choices[currentChoice]);
        DialogueManager.Instance.SelectChoice(choices[currentChoice]);
    }

    private void OnChoiceMenuStart(List<string> validChoices)
    {
        choicesDisplay.Display(validChoices, OnChoiceClicked);
        choices = validChoices;
        totalChoices = validChoices.Count;
        currentChoice = 0;
        Controller.OnSelect += SelectChoice;
        Controller.OnNavigateMenu += UpdateChoiceSelection;
    }

    private void OnChoiceMenuEnd()
    {
        choicesDisplay.Hide();
        Controller.OnSelect -= SelectChoice;
        Controller.OnNavigateMenu -= UpdateChoiceSelection;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= DisplayUI;
        DialogueManager.OnDialogueEnded -= HideUI;
        DialogueManager.OnChoiceMenuOpen -= OnChoiceMenuStart;
        DialogueManager.OnChoiceMenuClose -= OnChoiceMenuEnd;
    }

    private void OnDestroy()
    {
        Controller.OnSelect -= SelectChoice;
        Controller.OnNavigateMenu -= UpdateChoiceSelection;
        DialogueManager.OnTextUpdated -= textBoxDisplay.UpdateDialogueText;
    }
}