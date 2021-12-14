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
    private float wallJumpCooldown;
    float horizontalInput;

    public int damagePlayer;

    public LayerMask enemyLayers;
    //public Transform attackPoint;
    public GameObject attackPoint;
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
        playerLayer = LayerMask.NameToLayer("Player");
        jumpLayer = LayerMask.NameToLayer("JumpArea");        
    }
    private void Update()
    {
        Movement();
        Jump();
        UIController.instance.playerHealthBar.value =_health;
        if(UIController.instance.playerHealthBar.value == 0)
        {
            StartCoroutine("LoadScene2", 1f);
        }
        if (Time.time > nextTimeAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //if(!isAttackFirst && !isAttackSecond)
                //{
                Attack();
                
                nextTimeAttack = Time.time + 1 / attackRate;

            }
        }
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel);
    }
    IEnumerator LoadScene2()
    {
        yield return new WaitForSeconds(5.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
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
        attackPoint.SetActive(true);
        Invoke(nameof(DeActive), 1f);
        //Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //foreach (Collider2D enemy in hitEnemy)
        //{
        //    Debug.Log("Hit " + enemy.name);
        //    enemy.GetComponent<AIBoss>().takeDamage(damagePlayer);
        //}   
    }

    private void OnDrawGizmosSelected()
    {
        //if (attackPoint == null)
        //    return;
        //Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    public void hitDamage(int hitDamage)
    {
        _health -= hitDamage;
        if(_health <=0)
        {
            _health = 0;
            LoadScene2();
        }
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

        //if (collision.gameObject.tag == "Enemy")
        //    Debug.Log("Hit Boss");
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
        if (collision.tag == "item 1")
        {
            _health = 100;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "item2")
        {
            _health += 20;
            Destroy(collision.gameObject);
        }

    }
    public void DeActive()
    {
        attackPoint.SetActive(false);
    }

}
