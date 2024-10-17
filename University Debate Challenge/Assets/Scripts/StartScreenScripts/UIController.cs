using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClick() 
    {
        SceneManager.LoadScene("LobbyScreen");
    }

    public void QuitButtonClick() 
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    public void OptionsButtonClick()
    {
        SceneManager.LoadScene("OptionsScreen");
    }
}
