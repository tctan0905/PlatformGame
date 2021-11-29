using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float timeDestroy;
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public Vector2 moveDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeDestroy = 7f;
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
        timeDestroy -= Time.deltaTime;
        if(timeDestroy <=0)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
}
