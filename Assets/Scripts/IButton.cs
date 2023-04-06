using System;

public interface IButton
{
    event Action<IButton> OnClick;
    void Click();
    void ToggleSelected(bool isSelected);
    void Use();
}
