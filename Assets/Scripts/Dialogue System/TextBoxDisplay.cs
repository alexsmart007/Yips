using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextField;
    [SerializeField] Image textBoxImage;
    [SerializeField] Image voiceTextBoxImage;

    public void Display() => ToggleChildrenDisplay(true);

    public void UpdateDialogueText(string text, bool wickIsSpeaking)
    {
        dialogueTextField.text = text;

        if (textBoxImage != null)
        {
            textBoxImage.rectTransform.rotation = new Quaternion() { y = wickIsSpeaking ? 180 : 0 };
            voiceTextBoxImage.enabled = !text.Contains(":");
            textBoxImage.enabled = !voiceTextBoxImage.enabled;
        }
        
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