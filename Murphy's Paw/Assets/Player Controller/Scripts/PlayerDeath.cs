using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    public float deathHight;
    [HideInInspector] public UnityEvent isDead;
    [SerializeField] private FMODUnity.EventReference fmodSoundPath;
    private FMOD.Studio.EventInstance instance;
    private Transform _transform;

    private void Start() 
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(_transform.position.y < deathHight)
            Death();
    }

    public void Death()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
        isDead?.Invoke();
    }
}
