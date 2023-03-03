using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenuPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject CreditPanel;

    public AudioMixerGroup Music;
    public AudioMixerGroup SFX;

    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Settings()
    {
        SettingMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void Credits()
    {
        CreditPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void MainMenuSetting()
    {
        SettingMenuPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    } 
    public void MainMenuCredits()
    {
        CreditPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
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
