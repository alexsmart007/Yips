using UnityEngine;
using UnityEngine.Events;

public class EventButton : UIButton
{
    [SerializeField] UnityEvent eventToTrigger;

    public override void Use()
    {
        if (isSelected) eventToTrigger?.Invoke();
    }
}