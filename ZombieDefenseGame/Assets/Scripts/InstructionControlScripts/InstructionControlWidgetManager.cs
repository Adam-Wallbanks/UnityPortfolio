using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionControlWidgetManager : MonoBehaviour
{
  public void onBackClick()
    {
        SceneManager.LoadScene("StartUpScreen");

    }
}
