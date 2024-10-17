using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverAudioManager : MonoBehaviour
{
    public AudioSource GameOverScreenMusic;
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
            GameOverScreenMusic.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
        }
    }
}
