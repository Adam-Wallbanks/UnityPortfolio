using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateVolumeAsset
{
    // creating a new volume asset to be used when passing across scenes
    [MenuItem("Assets/Create/ScriptableObjects/Volume")]
    public static void CreateAsset()
    {
        VolumeSO asset = ScriptableObject.CreateInstance<VolumeSO>();
        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/New Volume.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}

