using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss : MonoBehaviour
{
    public GameObject[] bulletBoss;
    public float nexTime;
    public float fireRate;
    public float healthBoss;
    // Start is called before the first frame update
    void Start()
    {

        fireRate = 3f;
        nexTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nexTime)
        {
            Instantiate(bulletBoss[Random.Range(0, bulletBoss.Length)], transform.position, Quaternion.identity);
            nexTime = Time.time + fireRate;
        }
    }
}
