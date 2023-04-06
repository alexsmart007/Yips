using UnityEngine;

public class TitleUIButton : EventButton
{
    [SerializeField] GameObject starImage;

    public override void ToggleSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        starImage.SetActive(isSelected);
    }
}