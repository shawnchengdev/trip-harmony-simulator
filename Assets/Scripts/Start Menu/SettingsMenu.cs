using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Slider _volumeSlider;
    private Resolution[] _resolutions;
    private const int _minimumVolume = -80;

    private void Start()
    {
        Screen.fullScreen = true;
        SetPastSettings();
        SetUpResolutionDropDown();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Screen Width", resolution.width);
        PlayerPrefs.SetInt("Screen Height", resolution.height);
    }

    public void SetVolume(float volume)
    {
        float decibels = Mathf.Log10(volume) * 20f;
        if (decibels < _minimumVolume)
        {
            decibels = _minimumVolume;
        }
            
        _audioMixer.SetFloat("Volume", decibels);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("Is Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Is Fullscreen", 0);
        }       
    }

    private void SetPastSettings()
    {
        Screen.SetResolution(PlayerPrefs.GetInt("Screen Width"), PlayerPrefs.GetInt("Screen Height"), PlayerPrefs.GetInt("Is Fullscreen") == 1);
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        _audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }

    private void SetUpResolutionDropDown()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
}
