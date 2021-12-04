using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyShooter : MonoBehaviour
{
    public Transform playerTarget;
    public float nextTimeFire;
    public float fireRate;
    public float followRange;
    public float attackRange;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        nextTimeFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector2.Distance(transform.position, playerTarget.position);
        if (transform.position.x < playerTarget.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);

        }

        if (dir <= followRange && dir > attackRange)
        {
            Debug.Log("Follow Character");
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
        }
        else if (dir < attackRange && nextTimeFire < Time.time)
        {
            Debug.Log("Attack");
            nextTimeFire = Time.time + fireRate;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followRange);
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
    }
}
