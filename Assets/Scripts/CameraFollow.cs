using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTr;
    private float offsetY;
    private void Start()
    {
        offsetY = playerTr.position.y - transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, playerTr.position.y - offsetY, transform.position.z);
    }
}
