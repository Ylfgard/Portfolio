using UnityEngine.Events;
public class EventButton : InteractableButton
{
    public UnityEvent Clicked;
    protected sealed override void Interact() => Clicked?.Invoke();
}