using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FartListener : MonoBehaviour
{
    [SerializeField]
    private float fartDetectDistance = 6f;
    private FartManager fartManager;

    private void Awake()
    {
        fartManager = FindFirstObjectByType<LanceScript>().FartManager;
    }

    private void OnEnable()
    {
        fartManager.OnFart += FartManager_OnFart;
    }

    private void FartManager_OnFart(float intensity)
    {
        if (Vector3.Distance(fartManager.transform.position, transform.position) <= fartDetectDistance)
        {
            Debug.Log("Fart recognized");
        }
    }

    private void OnDisable()
    {
        fartManager.OnFart -= FartManager_OnFart;
    }

}
