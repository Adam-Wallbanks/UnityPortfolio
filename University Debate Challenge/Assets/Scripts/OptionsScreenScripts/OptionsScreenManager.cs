using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScreenManager : MonoBehaviour
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button colourBtn;
    [SerializeField] private Button topicsBtn;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(backButtonPress);
        colourBtn.onClick.AddListener(uiColourPress);
        topicsBtn.onClick.AddListener(customTopicPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void backButtonPress() {
        SceneManager.LoadScene("StartScreen");
    }

    void customTopicPress()
    {
        SceneManager.LoadScene("TopicSetterScreen");
    }

    void uiColourPress()
    {
        SceneManager.LoadScene("ColourSetterScreen");
    }
}
