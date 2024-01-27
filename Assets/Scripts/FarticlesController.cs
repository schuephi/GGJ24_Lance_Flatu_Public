using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FarticlesController : MonoBehaviour
{
    private ParticleSystem farticles;
    private FartManager fartManager;

    private void Awake()
    {
        farticles = GetComponent<ParticleSystem>();
        fartManager = GetComponentInParent<FartManager>();
    }

    private void OnEnable()
    {
        if (fartManager is not null) {
            fartManager.OnFart += FartManager_OnFart;
        }
    }

    private void FartManager_OnFart(float intensity)
    {
        farticles.Emit((int)(intensity * 30));
    }

    private void OnDisable()
    {
        if (fartManager is not null)
        {
            fartManager.OnFart -= FartManager_OnFart;
        }
    }
}
