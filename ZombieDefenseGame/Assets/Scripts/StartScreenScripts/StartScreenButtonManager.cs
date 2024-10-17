using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtonManager : MonoBehaviour
{
    public void PLayButtonClick()
    {
        SceneManager.LoadScene("GameScreen");

    }

    public void OptionsButtonClick() {
        SceneManager.LoadScene("OptionsScreen");
    }
    public void QuitButtonClick()
    {
        if (EditorApplication.isPlaying) { 
            EditorApplication.isPlaying = false;
        }
    }

    public void GuideButtonClick()
    {
        SceneManager.LoadScene("InstructionsScreen");
    }
}
