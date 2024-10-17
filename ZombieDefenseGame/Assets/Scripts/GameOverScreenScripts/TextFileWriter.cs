using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFileWriter : MonoBehaviour
{
    public VolumeSO scriptObj;

    public void onSaveScoreClick()
    {
        string path = "C:/Users/kgrei/unity-top-down-project/scores.txt";

        // find file path and write the username and number of kills  to it
        using (StreamWriter writer = new StreamWriter(path,true))
        {
            writer.WriteLine(scriptObj.username + " " + scriptObj.kills + "\n" );
        }
    }

}
