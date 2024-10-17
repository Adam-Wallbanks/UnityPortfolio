using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creating a scriptable object fro volume so it can be passed across scenes and scripts
[CreateAssetMenu(fileName = "New Volume", menuName = "Scriptable Objects/Volume", order = 1)]
public class VolumeSO : ScriptableObject
{
    public float volume = 1;
    public int kills = 0;
    public string username = "";
}

