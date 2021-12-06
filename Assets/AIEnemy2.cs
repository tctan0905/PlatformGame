using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy2 : MonoBehaviour
{
    public GameObject lazerPrefabs;
    public void Start()
    {
        Invoke(nameof(Show), 2f);
    }
    
    public void Show()
    {
        lazerPrefabs.SetActive(true);
        Invoke(nameof(Hide), 1.5f);
    }
    public void Hide()
    {
        lazerPrefabs.SetActive(false);

    }
}
