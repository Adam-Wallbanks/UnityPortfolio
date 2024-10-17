using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ContourSetter : MonoBehaviour
{
    public TMP_InputField contourInput;
    public TextMeshProUGUI contourDisplayText;
    public TextMeshProUGUI volumeDisplayText;
    public float contourDividerVal;
    public float volumeVal;
    public Button applyChangesBtn;
    public Button backBtn;
    public Slider volumeSlider;
    public Button infoBtnContour;
    public Button infoBtnVol;
    public GameObject rawImage;
    public GameObject popUpText;
    public GameObject volumePopUpText;
    public GameObject rawImageVol;
    private int activeCheck;
    private int activeCheck1;
    private void Start()
    {
        activeCheck = 0;
        LoadContourDiv();
        LoadVolumeVal(volumeSlider);
        applyChangesBtn.onClick.AddListener(onApplyChangesBtnPress);
        backBtn.onClick.AddListener(onBackButtonPress);
        volumeSlider.onValueChanged.AddListener(changeTextForVolume);
        infoBtnContour.onClick.AddListener(changeActiveStatus);
        infoBtnVol.onClick.AddListener(changeActiveStatusVol);
    }

    public void LoadContourDiv()
    {
        if (PlayerPrefs.HasKey("ContourDivFloat"))
        {
            contourDividerVal = PlayerPrefs.GetFloat("ContourDivFloat");
            contourInput.text = contourDividerVal.ToString();
        }
        else
        {
            contourDividerVal = 3;
            PlayerPrefs.SetFloat("ContourDivFloat", contourDividerVal);
            contourInput.text = contourDividerVal.ToString();
        }
    }

    public void LoadVolumeVal(Slider volumeSlider)
    {
        if (PlayerPrefs.HasKey("VolumeVal"))
        {
            volumeVal = PlayerPrefs.GetFloat("VolumeVal");
            volumeSlider.value = volumeVal;
            volumeDisplayText.text = ((int)(volumeVal * 100)).ToString();
        }
        else 
        {
            volumeVal = 1.0f;
            PlayerPrefs.SetFloat("VolumeVal", volumeVal);
            volumeSlider.value = volumeVal;
            volumeDisplayText.text = ((int)(volumeVal * 100)).ToString();
        }
    }

    public IEnumerator setContourAndVolume()
    {
        float newContourDivVal = 0;
        string newContourDivValString = contourInput.text;
        bool notEmpty = false;
        if(newContourDivValString.Length > 0) {
            notEmpty = true;
            newContourDivVal = float.Parse(newContourDivValString);
            PlayerPrefs.SetFloat("ContourDivFloat", newContourDivVal);
        }

        PlayerPrefs.SetFloat("VolumeVal",volumeSlider.value);

        float contourCheck = PlayerPrefs.GetFloat("ContourDivFloat");
        Debug.Log("this is te " + contourCheck);
        if (notEmpty)
        {
            contourDisplayText.text = "Changes Applied Successfully";
        }
        else
        {
            contourDisplayText.text = "Enter text into input field to make changes";
        }
        yield return new WaitForSeconds(2);
        contourDisplayText.text = "";

    }

    public void onApplyChangesBtnPress()
    {
        StartCoroutine(setContourAndVolume());
    }

    public void onBackButtonPress()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    void changeTextForVolume(float value)
    {
        volumeDisplayText.text = ((int)(volumeSlider.value * 100)).ToString();
    }

    void changeActiveStatus()
    {
        if (activeCheck == 1)
        {
            rawImage.SetActive(false);
            popUpText.SetActive(false);
            activeCheck = 0;
        }
        else 
        {
            rawImage.SetActive(true);
            popUpText.SetActive(true);
            activeCheck = 1;
        }
    }

    void changeActiveStatusVol()
    {
        if(activeCheck1 == 1)
        {
            rawImageVol.SetActive(false);
            volumePopUpText.SetActive(false);
            activeCheck1 = 0;
        }
        else
        {
            rawImageVol.SetActive(true);
            volumePopUpText.SetActive(true);
            activeCheck1 = 1;
        }
    }
    
}