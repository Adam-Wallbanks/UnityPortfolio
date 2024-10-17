using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenAudioManager : MonoBehaviour
{
    public AudioSource StartScreenMusic;
    public VolumeSO volumeVar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(volumeVar.volume == 0))
        {
            StartScreenMusic.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
        }
    }
}
