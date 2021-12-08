//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//public class AIEnemy2 : MonoBehaviour
//{
//    public GameObject lazerPrefabs;
//    float speed = 2.5f;
//    public float scaleY;
//    private bool isLaserAttack = false;
//    public GameObject target, targetBoss;
//    Vector2 Direction;
//    private Vector3 startPos;
//    public void Init()
//    {
//        StartCoroutine(ShootLaser());
//    }
    
//    IEnumerator ShootLaser()
//    {
//        yield return new WaitForSeconds(1f);
//        Vector2 targetPos = target.transform.position;
//        Direction = targetPos - (Vector2)lazerPrefabs.transform.position;
//        transform.up = -Direction;
//        //lazerPrefabs.transform.up
//        lazerPrefabs.SetActive(true);
//        lazerPrefabs.transform.DOScaleY(2.5f, speed / 2).OnComplete(() =>
//              {
//                  lazerPrefabs.transform.DOMove(targetPos, 0.5f);
//              });
//        StartCoroutine(LaserToTarget());
//        Debug.Log("SHOOT");

//    }
//    IEnumerator LaserToTarget()
//    {
//        yield return new WaitForSeconds(3f);
//        lazerPrefabs.SetActive(false);
//        lazerPrefabs.transform.localScale = new Vector3(1, 0.1f, 1);
//        lazerPrefabs.transform.position = transform.position;
//        StartCoroutine(MoveDown());
//    }
//    IEnumerator MoveDown()
//    {
//        yield return new WaitForSeconds(2f);
//    }
//}
