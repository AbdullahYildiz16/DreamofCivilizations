using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioSource menuAudioSource;
    private bool isLevelLoaded;

    private void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("audio_volume", 1);
    }

    public void OnPlayBtnClicked()
    {
        if (isLevelLoaded) return;
        SceneManager.LoadScene(GlobalPlayerPrefs.LevelIdx);
        isLevelLoaded = true;

    }

    public void OnExitBtnClicked()
    {
        Application.Quit();
    }

    public void OnAudioSliderValueChanged()
    {
        PlayerPrefs.SetFloat("audio_volume", audioSlider.value);
        menuAudioSource.volume = audioSlider.value;
    }
    
}
