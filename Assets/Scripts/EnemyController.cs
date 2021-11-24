using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healtEnemy;
    public Transform playerTarget;
    Rigidbody2D rb;
    public float moveSpeed;
    public float dir;
    PlayerMovement healthPlayer;
    public float nextHit;
    public float currentHit;
    public bool isFollow;
    private void Start()
    {
        playerTarget = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        healthPlayer = GetComponent<PlayerMovement>();
        currentHit = nextHit;
    }
    private void Update()
    {
        dir = Vector2.Distance(playerTarget.position, transform.position);
        currentHit -= Time.deltaTime;
        if(currentHit <=0)
        {
            if (transform.position.x - playerTarget.position.x <= 0.5f)
            {
                HitDamage();
                rb.velocity = Vector2.zero;
                currentHit = nextHit;
            }
            else
            {
                currentHit -= Time.deltaTime;
            }
            
        }
        if (dir < 5)
        {
            //transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            //transform.up = Vector2.MoveTowards(transform.up)
            if(!isFollow)
            {
                if (transform.position.x < playerTarget.position.x)
                {

                    rb.velocity = new Vector2(moveSpeed, 0);
                    transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    transform.localScale = new Vector2(-1, 1);
                }
                isFollow = true;

            }

        }
        else
        {
            if(dir <= 10 )
            {
                if(isFollow)
                {
                    if (transform.position.x < playerTarget.position.x)
                    {
                        rb.velocity = new Vector2(moveSpeed, 0);
                        transform.localScale = new Vector2(1, 1);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-moveSpeed, 0);
                        transform.localScale = new Vector2(-1, 1);
                    }
                }
                
            }
            if(dir > 10)
            {
                rb.velocity = Vector2.zero;
                isFollow = false;
                
            }
        }


    }

    public void takeDamage(int damage)
    {
        healtEnemy -= damage;
        if (healtEnemy <= 0)
        {
            
            Destroy(gameObject);
        }
    }
    public void HitDamage()
    {
        Debug.Log("Hit Player");
        //healthPlayer.hitDamage(10);
        //Debug.Log(healthPlayer._health);
    }
}
