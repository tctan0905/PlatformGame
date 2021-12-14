using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundControll : MonoBehaviour
{
    public AudioSource bgMusic;
    public Slider sl_bgMusicVolum;
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();   
    }

    
    public void Setting()
    {
        bgMusic.volume = sl_bgMusicVolum.value;

    }
}
