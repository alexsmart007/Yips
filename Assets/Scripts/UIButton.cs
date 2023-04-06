using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : MonoBehaviour, IButton
{
    public event Action<IButton> OnClick;
    protected bool isSelected;

    public void Click()
    {
        Use();
        OnClick?.Invoke(this);
    }

    public virtual void ToggleSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        transform.localScale = Vector3.one * (isSelected ? 1.2f : 1);
    }

    public abstract void Use();
}
