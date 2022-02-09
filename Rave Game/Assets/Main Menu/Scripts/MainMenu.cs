using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private GameObject _menuBody;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private bool _openOnStart;
    [SerializeField] private List<VolumeSLider> _volumeSliders;

    private bool _wasSetted = false;

    private void Awake()
    {
        if(_mediator != null)
        {
            _mediator.Subscribe<TreeLevelStagesCompleteCommand>(OpenWinWindow);
            _winWindow.SetActive(false);
        } 
        var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        Debug.Log(result);
        result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        Debug.Log(result);
        foreach(VolumeSLider volumeSLider in _volumeSliders)
            volumeSLider.StartSetVolume();
        _wasSetted = true;
        ChangeMenuActive(_openOnStart);
    }

    public void SetVolume()
    {
        if(_wasSetted)
        {
            foreach(VolumeSLider volumeSLider in _volumeSliders)
                volumeSLider.SetVolume();
        }
    }

    public void ChangeMenuActive(bool state)
    {
        _menuBody.SetActive(state);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OpenWinWindow(TreeLevelStagesCompleteCommand callback)
    {
        _winWindow.SetActive(true);
    }
}

[Serializable]
public class VolumeSLider
{
    [SerializeField] private string _plyaerPrefKey;
    [SerializeField] private Slider _slider;
    [SerializeField] private string _vcaName;
    private FMOD.Studio.VCA _vca;
    

    public void SetVolume()
    {
        float volume = _slider.value;
        PlayerPrefs.SetFloat(_plyaerPrefKey, volume);
        _vca.setVolume(volume);
    }

    public void StartSetVolume()
    {
        float volume = PlayerPrefs.GetFloat(_plyaerPrefKey, 0.5f);
        _vca = RuntimeManager.GetVCA(_vcaName);
        _vca.setVolume(volume);
        _slider.value = volume;
    }
}
