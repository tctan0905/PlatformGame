using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow2 : MonoBehaviour
{
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

        if (Vector3.Distance(temp, target.position) >= 1)
        {
            transform.DOMove(target.position + desiredPosition, 0.1f);
        }
    }
}
