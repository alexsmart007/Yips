using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesDisplay : MonoBehaviour
{
    [SerializeField] GameObject choiceTemplate;
    [SerializeField] float scaleFactor;

    List<TextMeshProUGUI> choicesText = new List<TextMeshProUGUI>();
    List<RectTransform> choices = new List<RectTransform>();

    Action<int> onSelect;

    public void Display(List<string> validChoices, Action<int> onSelect)
    {
        foreach(var choiceOption in validChoices)
        {
            GameObject instance = Instantiate(choiceTemplate, transform);
            var textBox = instance.transform.GetComponentInChildren<TextMeshProUGUI>();
            textBox.text = choiceOption;
            textBox.color = Color.gray;
            var uiButton = instance.transform.GetComponent<SimpleButton>();
            uiButton.OnClick += UiButton_OnClick;
            choicesText.Add(textBox);
            choices.Add(instance.GetComponent<RectTransform>());
        }

        this.onSelect = onSelect;
        if (choicesText.Count > 0) SelectChoice(0);
    }

    private void UiButton_OnClick(IButton obj)
    {
        onSelect(choices.Select(x => x.GetComponent<IButton>()).ToList().IndexOf(obj));
    }

    public void SelectChoice(int index)
    {
        choicesText.ForEach(choice => choice.color = Color.gray);
        choicesText[index].color = Color.black;
        if (choices.Count > 0)
        {
            choices.ForEach(x => x.localScale = Vector3.one);
            choices[index].localScale = Vector3.one * scaleFactor;
        }
    }

    public void Hide()
    {
        DestroyChildren();
    }

    private void DestroyChildren()
    {
        int children = transform.childCount - 1;
        while(children >= 0)
        {
            var uiButton = transform.GetChild(children).transform.GetComponent<SimpleButton>();
            uiButton.OnClick += UiButton_OnClick;
            Destroy(transform.GetChild(children).gameObject);
            children--;
        }
        choicesText.Clear();
        choices.Clear();
    }
}
