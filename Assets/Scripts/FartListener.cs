using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FartListener : MonoBehaviour
{
    [SerializeField]
    private float fartDetectDistance = 6f;
    private LanceScript lance;

    private void Start()
    {
        lance = FindFirstObjectByType<LanceScript>();

    }

    private void HandleFartNoisDetection()
    {
        if (Vector3.Distance(lance.transform.position, transform.position) <= fartDetectDistance)
        {
            Debug.Log("Fart recognized");
        }
    }
}
