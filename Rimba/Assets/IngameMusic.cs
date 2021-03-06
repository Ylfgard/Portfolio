using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMusic : MonoBehaviour
{
    [SerializeField] private string musicEvent;
    [SerializeField] private bool keepPlayingInNextScene;

    private FMOD.Studio.EventInstance instance;
    private int subscribeCount;

    private void Start()
    {
        SetMusic(musicEvent);

        if(keepPlayingInNextScene)
        {
            KeepPlaying();
        }
    }

    private void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SetMusic(string musicEvent)
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        instance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        //instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }

    private void KeepPlaying()
    {

        SceneManager.activeSceneChanged += DestroyMusic;
        DontDestroyOnLoad(gameObject);
        keepPlayingInNextScene = false;
    }

    private void DestroyMusic(Scene oldScene, Scene newScene)
    {
        Debug.Log(oldScene.name);
        Debug.Log(newScene.name);
        if(newScene.buildIndex == 0)
        {
            SceneManager.SetActiveScene(newScene);
            SceneManager.activeSceneChanged -= DestroyMusic;
            Destroy(gameObject);
        }
    }
}
