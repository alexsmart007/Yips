using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextField;
    [SerializeField] Image conversantPortrait;
    [SerializeField] Image wickPortrait;
    [SerializeField] Color nonSpeakerTint;

    public void Display() 
    {
        conversantPortrait.transform.localScale = Vector3.one * 1f;
        wickPortrait.transform.localScale = Vector3.one * .9f;
        ToggleChildrenDisplay(true);
    }

    public void UpdateDialogueText(string text, bool wickIsSpeaking)
    {
        dialogueTextField.text = text;   
        conversantPortrait.transform.localScale = Vector3.one * (wickIsSpeaking ? .9f : 1f);
        conversantPortrait.color = wickIsSpeaking ? nonSpeakerTint : Color.white;
        wickPortrait.transform.localScale = Vector3.one * (wickIsSpeaking ? 1f : .9f);
        wickPortrait.color = wickIsSpeaking ? Color.white : nonSpeakerTint;
    }

    public void Hide() => ToggleChildrenDisplay(false);

    private void ToggleChildrenDisplay(bool shouldDisplay)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldDisplay);
        }
    }
}