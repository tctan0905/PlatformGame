using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyShooter : MonoBehaviour
{
    public Transform playerTarget;
    public Transform firePoint;
    public GameObject bulletPrefabs;
    public float nextTimeFire;
    public float currentTimeFire;
    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        currentTimeFire = nextTimeFire;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeFire -= Time.deltaTime;
        if (transform.position.x < playerTarget.position.x)
        {
            transform.localScale = new Vector2(1, 3);
        }
        else
        {
            transform.localScale = new Vector2(-1, 3);

        }
        if (currentTimeFire <= 0)
        {
            if (Vector2.Distance(transform.position, playerTarget.position) < 10)
            {
                Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
                currentTimeFire = nextTimeFire;

            }
        }
        
    }
}
