using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameUIManager : MonoBehaviour
{
    public void loadResultsScene()
    {
        SceneManager.LoadScene("ResultsScene");
        Debug.Log("results btn pressed");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
