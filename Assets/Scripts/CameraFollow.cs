using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    //dahkd
    Vector3 desiredPosition;
    public Transform target;
    private Vector3 temp;
    private void OnEnable()
    {
        desiredPosition = transform.position - target.position;
    }

    private void LateUpdate()
    {
        temp = transform.position;
        temp.z = target.position.z;
        Debug.Log(Vector3.Distance(target.position, temp));
        if (Vector3.Distance(temp, target.position) >= 10)
        {
            transform.DOKill();
            transform.DOMove(target.position + desiredPosition, 0.5f);
        }
    }
}
