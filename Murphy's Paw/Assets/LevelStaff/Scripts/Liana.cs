using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liana : MonoBehaviour, IInteractable
{
    [SerializeField] private FMODUnity.EventReference fmodSoundPath;
    private FMOD.Studio.EventInstance instance;
    public void Activate()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
        Destroy(gameObject);
    }
}

