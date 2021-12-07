using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject target;

    Vector2 Direction;
    float moveSpeed = 1f;
    private void Start()
    {
        Vector2 targetPos = target.transform.position;
        Direction = targetPos - (Vector2)transform.position;
        transform.up = -Direction;
        
    }
    public void Update()
    {
        float scaleLaser = Mathf.Lerp(transform.localScale.y, transform.localScale.y + 2, Time.deltaTime * moveSpeed);
        for (int i = 0; i < 2; i++)
        {
            transform.localScale = this.transform.localScale + new Vector3(0, 1f, 0) * Time.deltaTime;
        }
        Invoke(nameof(DeActive), 1.5f);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Trigger");
        }
    }
    public void DeActive()
    {
        transform.localScale = new Vector3(1, 0.1f, 1);
    }
}
