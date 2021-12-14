using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossSound : MonoBehaviour
{
    public GameObject bgSound;
    public GameObject bossSound;
    public Slider sl_BossSound;

    private void Start()
    {
    }
   
    public void Setting()
    {
        bossSound.GetComponent<AudioSource>().volume = sl_BossSound.value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bgSound.GetComponent<AudioSource>().Pause();
            bossSound.GetComponent<AudioSource>().Play();
        }    
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        bgSound.GetComponent<AudioSource>().Pause();
    //        bossSound.GetComponent<AudioSource>().Play();
    //    }
    //}
}
