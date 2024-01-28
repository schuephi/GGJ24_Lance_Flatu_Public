using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FartManager), typeof(LanceScript))]
public class FartEngine : MonoBehaviour
{
    private FartManager fartManager;
    private LanceScript lanceScript;
    private bool isFarting;

    private void Awake()
    {
        fartManager = GetComponent<FartManager>();
        lanceScript = GetComponent<LanceScript>();
    }

    private void OnEnable()
    {
        fartManager.OnFart += FartManager_OnFart;
        fartManager.OnFartStopped += FartManager_OnFartStopped;
    }

    private void FixedUpdate()
    {
        if (isFarting)
        {
            lanceScript.Flatulence -= Time.fixedDeltaTime;
        }
        if (lanceScript.Flatulence <= 0)
        {
            fartManager.StopFart();
            lanceScript.Flatulence = 0;
        }
    }

    private void FartManager_OnFart(float intensity)
    {
        isFarting = true;
    }

    private void FartManager_OnFartStopped(float intensity)
    {
        isFarting = false;
    }

    private void OnDisable()
    {
        fartManager.OnFart -= FartManager_OnFart;
        fartManager.OnFartStopped -= FartManager_OnFartStopped;
    }
}
