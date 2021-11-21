using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFuntions : MonoBehaviour
{
    [SerializeField] private bool isStartMenu;
    [SerializeField] private GameObject menuFone;
    [SerializeField] private GameObject settingsFone;
    [SerializeField] private Slider textShowSpeedSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider dialogsVolumeSlider;
    [SerializeField] private SceneChanger sceneChanger;

    /*private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus musicVca;
    private FMOD.Studio.Bus dialogsBus;
    
    private FMOD.Studio.Bus UIBus;
    private FMOD.Studio.Bus voiceBus;
    private FMOD.Studio.Bus ambienceBus;*/

    /*public void SetVolume()
    {
        float masterBus = musicVolumeSlider.value;
        masterBus.setVolume(masterBus);
        PlayerPrefs.SetFloat("musicVolume", masterBus);
        PlayerPrefs.Save();
    }

    public void GetVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        musicVolumeSlider.value = musicVolume;
    }*/

    public void LoadLevel(string levelName) 
    { 
       sceneChanger.ChangeScene(levelName);
    }

    public void LoadLastPlayedLevel() 
    { 
        string loadedLevelName;
        loadedLevelName = PlayerPrefs.GetString("lastGameScene", "Level_1");
        sceneChanger.ChangeScene(loadedLevelName);
    }

    public void RestartLevel() 
    { 
        sceneChanger.ChangeScene(SceneManager.GetActiveScene().name); 
    }

    public void OpenSettings()
    {
        settingsFone.SetActive(true);
    }
    
    public void ChangeTextShowSpeed()
    {
        float textShowSpeed = textShowSpeedSlider.value;
        PlayerPrefs.SetFloat("TextShowSpeed", textShowSpeed);
    }
    
    public void CloseSettings()
    {
        settingsFone.SetActive(false);
    }
    
    public void OpenMenu()
    {
        GamePauser.StopGame(gameObject);
        CloseSettings();
        menuFone.SetActive(true);
    }

    public void CloseMenu()
    {
        GamePauser.ContinueGame(gameObject);
        CloseSettings();
        menuFone.SetActive(false);
    }

    public void Exit() 
    { 
        Application.Quit(); 
    }

    /*private void Awake()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/music");
    }*/

    private void Start() 
    {
        if(isStartMenu) 
        {
            OpenMenu();
        }
        else 
        {
            PlayerPrefs.SetString("lastGameScene", SceneManager.GetActiveScene().name);
            CloseMenu();
        }
    }

    private void Update() 
    {
        if(!isStartMenu && Input.GetKeyDown(KeyCode.Escape))
            if(menuFone.activeSelf) CloseMenu();
            else OpenMenu();
    }
    
    private void OnDestroy() 
    {
        CloseMenu();
    }
}
