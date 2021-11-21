using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBunner : MonoBehaviour
{
    [SerializeField] private GameObject adWindow;
    [SerializeField] private FMODUnity.EventReference fmodSoundPath;
    private FMOD.Studio.EventInstance instance;

    private void Start() 
    {
        CloseAd();
    }

    public void PlayAd()
    {
        GamePauser.StopGame(gameObject);
        adWindow.SetActive(true);
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
    }

    public void RestartAd()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
    }

    public void CloseAd()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        GamePauser.ContinueGame(gameObject);
        adWindow.SetActive(false);
    }
}
