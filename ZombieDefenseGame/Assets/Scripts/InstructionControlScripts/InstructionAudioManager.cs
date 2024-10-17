using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionAudioManager : MonoBehaviour
{
    public AudioSource instructionScreenMusic;
    public VolumeSO volumeVar;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!(volumeVar.volume == 0)) { 
            instructionScreenMusic.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
        }
    }
}
