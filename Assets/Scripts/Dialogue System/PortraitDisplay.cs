using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortraitDisplay : MonoBehaviour
{
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] ImageDatabase imageDatabase;

    public void Display(string characterName)
    {
        ToggleChildrenDisplay(true);
        portrait.sprite = imageDatabase.GetPortrait(characterName);
        portrait.enabled = portrait.sprite != null;
        textField.text =  characterName[0].ToString().ToUpper() + characterName.Substring(1);
    }

    public void Hide()
    {
        ToggleChildrenDisplay(false);
    }

    private void ToggleChildrenDisplay(bool shouldDisplay)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldDisplay);
        }
    }
}
