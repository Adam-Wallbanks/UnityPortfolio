using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScreenUiManager : MonoBehaviour
{
    public Button tractorLevelBtn;
    public Button rocketLevelBtn;
    public Button dressLevelBtn;
    public Button backBtn;
    // Start is called before the first frame update
    void Start()
    {
        tractorLevelBtn.onClick.AddListener(onTractorBtnClick);
        rocketLevelBtn.onClick.AddListener(onRocketBtnClick);
        dressLevelBtn.onClick.AddListener(onDressBtnClick);
        backBtn.onClick.AddListener(onBackButtonPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTractorBtnClick()
    {
        SceneManager.LoadScene("TractorScene");
    }

    public void onRocketBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void onDressBtnClick()
    {
        SceneManager.LoadScene("DressChangeScene");
    }

    public void onBackButtonPress()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
