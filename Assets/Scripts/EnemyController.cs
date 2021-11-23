using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healtEnemy;

    public void takeDamage(int damage)
    {
        healtEnemy -= damage;
        if (healtEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
