using UnityEngine;

public class TestDialogueEvent : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private string massage;
    public void PlayEvent(float duration)
    {
        Debug.Log(massage);
    }

    public void StopEvent()
    {
        Debug.Log("Event stopped");;
    }
}
