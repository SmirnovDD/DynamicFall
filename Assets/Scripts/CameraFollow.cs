using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTr;
    private float offsetY;
    private Transform floorTransform;
    private Vector3 floorPos;
    private IEnumerator Start()
    {
        offsetY = playerTr.position.y - transform.position.y;
        floorPos = new Vector3(0, -100, 0);

        yield return new WaitForEndOfFrame();
        floorTransform = GameObject.FindGameObjectWithTag("End").transform;
        floorPos = floorTransform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerTr.position.y - floorPos.y > 7.25f) 
            transform.position = new Vector3(transform.position.x, playerTr.position.y - offsetY, transform.position.z);
    }
}
