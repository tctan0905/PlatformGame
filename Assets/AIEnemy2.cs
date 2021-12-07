using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AIEnemy2 : MonoBehaviour
{
    public GameObject lazerPrefabs;
    float speed = 2.5f;
    public float scaleY;
    private bool isLaserAttack = false;
    public GameObject target, targetBoss;
    Vector2 Direction;
    private Vector3 startPos;
    private bool isMoveUp = false;
    public Ease easeAnim;
    public void Start()
    {
        startPos = transform.position;
        //transform.position = targetBoss.transform.position;
        Invoke(nameof(Show),2f);
        //lazerPrefabs.transform.DOMove(()=> { });
        
    }

    public void Update()
    {
        //if (isLaserAttack)
        //{
        //    scaleY = Mathf.Lerp(lazerPrefabs.transform.localScale.y, lazerPrefabs.transform.localScale.y + 2f, speed * Time.deltaTime);
        //    lazerPrefabs.transform.localScale = new Vector3(1,scaleY,1);
        //    StartCoroutine(LaserToTarget());

        //}

        //if(isMoveUp)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * 10 * Time.deltaTime);
        //}
        //else
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, targetBoss.transform.position, speed * 10 * Time.deltaTime);
        //}
    }
    public void Show()
    {
        StartCoroutine(MoveUpAndShoot());
       
        //StartCoroutine(LaserToTarget());
    }
    IEnumerator LaserToTarget()
    {
        yield return new WaitForSeconds(5f);
        lazerPrefabs.SetActive(false);
        lazerPrefabs.transform.localScale = new Vector3(1, 0.1f, 1);
        lazerPrefabs.transform.position = transform.position;
        //lazerPrefabs.transform.DOScaleY(0.1f, speed/2);

        isLaserAttack = false;
        StartCoroutine(MoveDown());
    }
    IEnumerator MoveUpAndShoot()
    {
        isMoveUp = true;
        yield return new WaitForSeconds(1.5f);
        Vector2 targetPos = target.transform.position;
        Vector2 demo = (lazerPrefabs.transform.position - target.transform.position).normalized;
        Direction = targetPos - (Vector2)lazerPrefabs.transform.position;
        transform.up = -Direction;
        lazerPrefabs.SetActive(true);
        lazerPrefabs.transform.DOScaleY(2.5f, speed).OnStepComplete(() => { lazerPrefabs.transform.DOMove(new Vector2(demo.x, demo.y), 2.5f); });
        //lazerPrefabs.transform.position = new Vector2(demo.x, demo.y);
        isLaserAttack = true;
        StartCoroutine(LaserToTarget());
    }
    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(2f);
        isMoveUp = false;
        Invoke(nameof(Show), 2f);
    }
}
