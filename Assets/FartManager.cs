using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ACTIVE_MODE
{
    STARTING,
    LOOPING,
    STOPPING
}

public class ActiveFart
{
    public FartData Fart;
}

public class FartManager : MonoBehaviour
{
    public event Action<float> OnFart = delegate { };
    public event Action<float> OnFartStopped = delegate { };
    public AudioSource FartSource;

    private ActiveFart ActiveFart = new ActiveFart();
    public List<FartData> Farts;

    private float StartTime;
    public ACTIVE_MODE ActiveMode;

    public FartData SelectRandomFart()
    {
        var randomIndex = Mathf.Clamp(Mathf.RoundToInt(UnityEngine.Random.Range(0, Farts.Count)), 0, Farts.Count - 1);
        return this.Farts[randomIndex];
    }

    public FartData SelectFartByIntensity(float intensity)
    {
        return Farts.Where(x => x.Intensity <= intensity).OrderBy(x => (intensity - x.Intensity)).First();
    }

    public void Fart(float intensity)
    {
        ActiveFart.Fart = SelectFartByIntensity(intensity);
        if (ActiveFart == null) return;

        FartSource.clip = ActiveFart.Fart.StartFart;
        FartSource.Play();

        StartTime = Time.time;

        OnFart(intensity);
    }

    public void FartSingle()
    {
        var fart = SelectRandomFart();
        FartSource.clip = fart.EndFart;
        FartSource.loop = false;
        FartSource.Play();
    }

    public void Update()
    {
        if (ActiveFart.Fart == null) return;

        switch (ActiveMode)
        {
            case ACTIVE_MODE.STARTING:
                {
                    if ((Time.time - StartTime) > ActiveFart.Fart.StartFart.length)
                    {
                        FartSource.Stop();
                        FartSource.clip = ActiveFart.Fart.LoopFart;
                        FartSource.loop = true;
                        FartSource.Play();
                        ActiveMode = ACTIVE_MODE.LOOPING;
                    }
                    break;
                }
            case ACTIVE_MODE.LOOPING:
                {
                    break;
                }
            case ACTIVE_MODE.STOPPING:
                {
                    break;
                }
        }

     

    }

    public void StopFart()
    {
        FartSource.Stop();
        FartSource.clip = ActiveFart.Fart.EndFart;
        FartSource.loop = false;
        FartSource.Play();
        ActiveMode = ACTIVE_MODE.STARTING;    
        OnFartStopped(0);
        ActiveFart.Fart = null;
    }
}
