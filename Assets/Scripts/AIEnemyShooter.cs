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
    private Animator FEanim;
    public int feDamage;
    private Vector2 moveDirection;
    private bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        nextTimeFire = Time.time;
        FEanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector2.Distance(transform.position, playerTarget.position);
        if(!isAttack)
        {
            if (transform.position.x < playerTarget.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);

            }

            if (dir <= followRange && dir > attackRange)
            {
                Debug.Log("Follow Character");
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed/2f * Time.deltaTime);
            }
            else if (dir < attackRange && nextTimeFire < Time.time)
            {
                StartCoroutine(FEAttack2());
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
 
    //public void FEAttack()
    //{
    //    FEanim.SetTrigger("FEAttack");
    //    var tranformPlayerAgo = playerTarget;
    //    moveDirection = (tranformPlayerAgo.position - transform.position).normalized * moveSpeed*5;
    //    rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    //    Debug.Log("Attack");
    //    //nextTimeFire = Time.time + fireRate;
    //}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Hit Player");
            Destroy(gameObject,0.2f);
            other.gameObject.SendMessage("hitDamage", 5);
        }
        if(other.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Hit Ground");
            Destroy(gameObject,0.2f);
        }
    }
    IEnumerator FEAttack2()
    {
        yield return new WaitForSeconds(0.5f);
        if(!isAttack)
        {
            FEanim.SetTrigger("FEAttack");
            var tranformPlayerAgo = playerTarget;
            moveDirection = (tranformPlayerAgo.position - transform.position).normalized * moveSpeed *10f;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Debug.Log("Attack");
            isAttack = true;
        }
       
    }

}
