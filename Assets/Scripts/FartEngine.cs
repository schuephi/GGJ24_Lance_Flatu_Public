using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FartManager), typeof(LanceScript))]
public class FartEngine : MonoBehaviour
{
    private FartManager fartManager;

    private void Awake()
    {
        fartManager = GetComponent<FartManager>();
    }

    private void OnEnable()
    {
        fartManager.OnFart += FartManager_OnFart;
        fartManager.OnFartStopped += FartManager_OnFartStopped;
    }

    private void FartManager_OnFart(float obj)
    {
        throw new System.NotImplementedException();
    }

    private void FartManager_OnFartStopped(float obj)
    {
        throw new System.NotImplementedException();
    }
}
