using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EnterUsernameUIManager : MonoBehaviour
{
    public VolumeSO user;
    public TMP_InputField userInput;

    public void onSubmitClick()
    {
        SceneManager.LoadScene("StartUpScreen");
        user.username = userInput.text;
    }
}
