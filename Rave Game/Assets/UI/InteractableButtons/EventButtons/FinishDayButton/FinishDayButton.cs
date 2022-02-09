using FMODUnity;
using UnityEngine;
public class FinishDayButton : EventButton
{
    [SerializeField]
    private EventReference _endDaySound;
    private new void OnEnable()
    {
        base.OnEnable();
        Clicked.AddListener(PlaySoundOnDayFinish);
    }
    private new void OnDisable()
    {
        base.OnDisable();
        Clicked.RemoveListener(PlaySoundOnDayFinish);
    }
    private void PlaySoundOnDayFinish()
    {
        RuntimeManager.PlayOneShot(_endDaySound);
    }
}