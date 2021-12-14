using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomInOut : MonoBehaviour
{
    [SerializeField] bool isZoomOut = false;

    public void Update()
    {
        if (isZoomOut)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 30f, Time.deltaTime * 2f);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 20f, Time.deltaTime * 2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isZoomOut = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isZoomOut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isZoomOut = false;
    }
}
