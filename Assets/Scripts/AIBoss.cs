using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss : MonoBehaviour
{
    public GameObject[] bulletBoss;
    public float nexTime;
    public float fireRate;
    public float healthBoss;
    public float lineOfSite;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fireRate = 3f;
        nexTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if(Time.time > nexTime && distanceFromPlayer < lineOfSite)
        {
                Instantiate(bulletBoss[Random.Range(0, bulletBoss.Length)], transform.position, Quaternion.identity);
                nexTime = Time.time + fireRate;            
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    public void takeDamage(int damage)
    {
        healthBoss -= damage;
        if (healthBoss <= 0)
        {
            Debug.Log("BOSS DIE");
        }
    }
}
