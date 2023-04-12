using TMPro;
using UnityEngine;

public class TitleUIButton : EventButton
{
    [SerializeField] TextMeshProUGUI text;

    public override void ToggleSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        text.fontSize = isSelected ? 110 : 90;
    }
}