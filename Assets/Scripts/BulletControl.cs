using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletControl : MonoBehaviour
{

    public void Update()
    {
        //float scaleLaser = Mathf.Lerp(transform.localScale.y, transform.localScale.y + 2, Time.deltaTime * moveSpeed);

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Trigger");
        }
    }
    
}
