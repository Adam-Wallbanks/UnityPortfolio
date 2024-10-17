using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WriteToFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FileWriting(List<Vector3> con1, List<Vector3> con2, List<Vector3> con3, List<Vector3> con4, List<Vector3> con5, List<Vector3> con6, List<Vector3> con7, List<Vector3> con8)
    {
        SaveVector3List("contour1", con1);
        SaveVector3List("contour2", con2);
        SaveVector3List("contour3", con3);
        SaveVector3List("contour4", con4);
        SaveVector3List("contour5", con5);
        SaveVector3List("contour6", con6);
        SaveVector3List("contour7", con7);
        SaveVector3List("contour8", con8);
    }

    public void SaveVector3List(string key, List<Vector3> vectors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in vectors)
        {
            sb.Append(v.x).Append(",").Append(v.y).Append(",").Append(v.z).Append("|");
        }
        if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
        PlayerPrefs.SetString(key, sb.ToString());
        PlayerPrefs.Save(); 
    }

    public List<Vector3> LoadVector3List(string key)
    {
        List<Vector3> vectors = new List<Vector3>();
        if (PlayerPrefs.HasKey(key))
        {
            string[] vectorStrings = PlayerPrefs.GetString(key).Split('|');
            foreach (string vectorString in vectorStrings)
            {
                string[] values = vectorString.Split(',');
                if (values.Length == 3)
                {
                    Vector3 vector = new Vector3(
                        float.Parse(values[0]),
                        float.Parse(values[1]),
                        float.Parse(values[2]));
                    vectors.Add(vector);
                }
            }
        }
        return vectors;
    }



}
