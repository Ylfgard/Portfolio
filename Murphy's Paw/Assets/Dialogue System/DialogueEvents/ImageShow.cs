using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageShow : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private bool state;
    [SerializeField] private Image image;

    public void PlayEvent(float duration)
    {
        image.enabled = state;
    }

    public void StopEvent()
    {
        Debug.Log("Event stopped");;
    }
}
