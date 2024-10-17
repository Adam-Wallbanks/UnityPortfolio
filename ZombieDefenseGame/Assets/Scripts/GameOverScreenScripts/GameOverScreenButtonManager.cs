using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenButtonManager : MonoBehaviour
{
    public VolumeSO killNum;
    public TextMeshProUGUI killText;

    public void onPlayAgainClick()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void onQuitClick() 
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            killNum.kills = 0;
            killNum.volume = 0;
            killNum.username = "";
        }
    }

    void Start()
    {
        killText.text = "Kills : " + killNum.kills;    
    }

    void Update()
    {
        killText.text = "Kills : " + killNum.kills;
    }

}
