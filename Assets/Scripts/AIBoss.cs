using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[Serializable]
public class Eye
{
    public GameObject eye;
    public GameObject lazer;
    public Vector3 endPos;
}
public class AIBoss : MonoBehaviour
{
    public float nexTime;
    public float fireRate;
    public float healthBoss;
    public float lineOfSite;
    private Transform player,startPos;
    public List<Eye> listEye;
    private bool isMoveUp = false;
    float speed = 2.5f;
    [SerializeField]bool isShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nexTime = Time.time;
        foreach (var eye in listEye)
        {
            eye.endPos = eye.eye.transform.position;
            eye.eye.transform.position = transform.position;

        }
    }

    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    public void takeDamage(int damage)
    {
        healthBoss -= damage;
        if (healthBoss <= 0)
        {
            Debug.Log("BOSS DIE");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (Time.time > nexTime && !isShoot && healthBoss >0)
        {
            MoveUpAndShoot();
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (Time.time > nexTime && !isShoot && healthBoss > 0)
        {
            MoveUpAndShoot();
        }
    }
    public void MoveUpAndShoot()
    {
        isShoot = true;
        foreach (var eye in listEye)
        {
            eye.eye.transform.DOMove(eye.endPos, 1f).OnComplete(() =>
            {
                Vector2 targetPos = player.transform.position;
                eye.lazer.transform.position = eye.eye.transform.position;
                Vector2 Direction = targetPos - (Vector2)eye.lazer.transform.transform.position;
                eye.eye.transform.up = -Direction;
                eye.lazer.SetActive(true);
                eye.lazer.transform.DOScaleY(2.5f, speed / 10).OnComplete(() =>
                {
                    eye.lazer.transform.DOMove(targetPos, 0.2f).OnComplete(() =>
                    {
                        eye.lazer.SetActive(false);
                        eye.lazer.transform.localScale = new Vector3(1, 0.1f, 1);
                        eye.eye.transform.DOMove(transform.position, 1f).OnComplete(()=>
                        {
                            isShoot = false;
                            nexTime = Time.time + fireRate;
                        });
                        
                    });
                });
            });
        }
    }
}
