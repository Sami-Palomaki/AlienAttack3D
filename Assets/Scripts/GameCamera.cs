using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 cameraTarget;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }


    void Update()
    {
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 8);
    }
}
