using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject SettingPanel;

    public AudioMixerGroup Music;
    public AudioMixerGroup SFX;

    public static bool pauseMenuOn = false;

    public void Update()
    {
        if (pauseMenuOn && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        else if (!pauseMenuOn && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        pauseMenuOn = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        pauseMenuOn = false;
    }
    public void Settings()
    {
        SettingPanel.SetActive(true);
        PausePanel.SetActive(false);
    }
    public void Back()
    {
        SettingPanel.SetActive(false);
        PausePanel.SetActive(true);
    }
    public void VolumeChangeMusic(float volume)
    {
        Music.audioMixer.SetFloat("VolumeMusic", volume);
    }
    public void VolumeChangeSFX(float volume)
    {
        SFX.audioMixer.SetFloat("VolumeSFX", volume);
    }
}
