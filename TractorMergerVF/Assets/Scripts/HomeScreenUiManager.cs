using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenUiManager : MonoBehaviour
{
    public Button levelSelectBtn;
    public Button settingsBtn;
    public Button quitBtn;
    public float contourDividerVal;
    public float volumeVal;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ContourDivFloat"))
        {
            contourDividerVal = PlayerPrefs.GetFloat("ContourDivFloat");
        }
        else
        {
            contourDividerVal = 3;
            PlayerPrefs.SetFloat("ContourDivFloat", contourDividerVal);
        }
        if (PlayerPrefs.HasKey("VolumeVal"))
        {
            volumeVal = PlayerPrefs.GetFloat("VolumeVal");
        }
        else
        {
            volumeVal = 1.0f;
            PlayerPrefs.SetFloat("VolumeVal", volumeVal);
        }

        levelSelectBtn.onClick.AddListener(toLevelSelect);
        settingsBtn.onClick.AddListener(toSettings);
        quitBtn.onClick.AddListener(toQuit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void toSettings()
    {
        SceneManager.LoadScene("SettingsScreen");
    }

    public void toQuit()
    {
        if(Application.isPlaying)
        {
            Application.Quit();
        }
    }
}
