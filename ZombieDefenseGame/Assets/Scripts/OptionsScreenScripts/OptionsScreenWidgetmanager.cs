using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScreenWidgetmanager : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI sliderText;
    public VolumeSO volumeVar;
    private int displayVolume;

    public void BackButtonClick() {
        SceneManager.LoadScene("StartUpScreen");
    }

    void Update()
    {
        float sliderVal = volumeSlider.value;
        displayVolume = (int)(sliderVal * 100);
        sliderText.text = displayVolume.ToString();
    }

    public void ApplyButtonClick()
    {
        volumeVar.volume = volumeSlider.value;
        Debug.Log("new volume: "+ volumeVar.volume);
    }
}
