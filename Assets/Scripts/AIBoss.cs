using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

[Serializable]
public class Eye
{
    public GameObject eye;
    public GameObject lazer;
    public Vector3 endPos;
}
[Serializable]
public class Enemy
{
    public GameObject enemy;
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
    public List<Enemy> listEnemy;
    private bool isMoveUp = false;
    float speed = 2.5f;
    [SerializeField]bool isShoot = false;

    public Slider bossHealthBar;
    public GameObject winGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        winGamePanel.SetActive(false);
        bossHealthBar.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nexTime = Time.time;
        foreach (var eye in listEye)
        {
            eye.endPos = eye.eye.transform.position;
            eye.eye.transform.position = transform.position;

        }
        foreach (var enemy in listEnemy)
        {
            enemy.endPos = enemy.enemy.transform.position;
            enemy.enemy.transform.position = transform.position;

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
        bossHealthBar.value = healthBoss;

        if (healthBoss <= 0)
        {
            Time.timeScale = 0;
            winGamePanel.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (Time.time > nexTime && !isShoot && healthBoss >0)
        {
            MoveUpAndShoot();
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.value = healthBoss;
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
    public void OnTriggerExit2D(Collider2D collision)
    {
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
                            foreach (var enemy in listEnemy)
                            {
                                enemy.enemy.transform.DOMove(enemy.endPos, 1f).OnComplete(()=> 
                                {
                                    var sequence = DOTween.Sequence();
                                    foreach (var enemy in listEnemy)
                                    {
                                        targetPos = player.transform.position;
                                        sequence.Append(enemy.enemy.transform.DOMove(targetPos, 0.5f).OnComplete(() =>
                                        {
                                            enemy.enemy.transform.position = transform.position;
                                        }));
                                    }
                                    sequence.OnComplete(() =>
                                    {
                                        isShoot = false;
                                        nexTime = Time.time + fireRate;
                                    });
                                });
                            }
                        });
                        
                    });
                });
            });
            
        }
    }
}
