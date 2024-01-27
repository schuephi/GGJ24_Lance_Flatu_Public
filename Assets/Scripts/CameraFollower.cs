using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform transformToFollow;
    private Vector3 offset;

    private void Awake()
    {
        transformToFollow = FindFirstObjectByType<LanceScript>().transform;
        offset = new Vector3(0, 0, -10);
    }

    private void LateUpdate()
    {
        transform.position = transformToFollow.position + offset;
    }
}
