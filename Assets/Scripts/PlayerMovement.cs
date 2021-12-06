using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PanelTransition;

    int playerLayer, jumpLayer;

    public int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    bool isGround = false;
    bool doubleJumpAllowed = false;
    bool attackAllowed = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    float horizontalInput;

    public int damagePlayer;

    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextTimeAttack = 0f;
    public bool isAttackFirst = false;
    public bool isAttackSecond = false;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        jumpLayer = LayerMask.NameToLayer("JumpArea");        
    }
    private void Update()
    {
        Movement();
        Jump();
        
        if (Time.time > nextTimeAttack)
        {
            //isAttackFirst = false;
            //isAttackSecond = false;
            if (Input.GetMouseButtonDown(0))
            {
                //if(!isAttackFirst && !isAttackSecond)
                //{
                Attack();
                //Attack2();
                //}

                //if(isAttackFirst && !isAttackSecond)
                //{
                //    Attack2();
                //}
                //isDoubleAttack = false;
                nextTimeAttack = Time.time + 1 / attackRate;

            }
        }

       

    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");

    }
    IEnumerator LoadScene2()
    {
        yield return new WaitForSeconds(6f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            doubleJumpAllowed = true;
        }
        else
            isGround = false;
    }
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);
        #region Flip Character
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        #endregion
       
        // set animator
        anim.SetBool("Run", horizontalInput != 0);
    }

    void Jump()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            isGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAllowed)
        {
            rb.velocity = new Vector2(rb.velocity.x , _jumpPower);
            doubleJumpAllowed = false;
        }

        anim.SetBool("Grounded", isGround);


        if (rb.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, jumpLayer, true);
        else
            Physics2D.IgnoreLayerCollision(playerLayer, jumpLayer, false);

    }
    void Attack()
    {     
        anim.SetTrigger("Attack");
        isAttackFirst = true;
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<EnemyController>().takeDamage(damagePlayer);
        }   
    }
    void Attack2()
    {
        Collider2D hitBoss = Physics2D.OverlapCircle(attackPoint.position, attackRange, 11);
        hitBoss.GetComponent<AIBoss>().takeDamage(damagePlayer);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    public void hitDamage(int hitDamage)
    {
        _health -= hitDamage;
        if(_health <=0)
        {
            Debug.Log("DIE");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            PanelTransition.SetActive(true);
            StartCoroutine(LoadScene2());
        }
        if (collision.tag == "Die")
        {
            Invoke("ReloadScene", 1f);
        }

    }

  
}
