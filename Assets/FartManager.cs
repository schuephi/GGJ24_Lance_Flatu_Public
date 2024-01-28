using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIVE_MODE
{
    STARTING,
    LOOPING,
    STOPPING
}

public class FartManager : MonoBehaviour
{
    public event Action<float> OnFart = delegate { };
    public event Action<float> OnFartStopped = delegate { };
    public AudioSource FartSource;

    public FartData ActiveFart;
    public List<FartData> Farts;
    public List<AudioClip> ShortFarts;
    public List<AudioClip> LongFarts;

    private float StartTime;
    public ACTIVE_MODE ActiveMode;

    public void SelectRandomFart()
    {
        var randomIndex = Mathf.Clamp(Mathf.RoundToInt(UnityEngine.Random.Range(0, Farts.Count)), 0, Farts.Count - 1);
        ActiveFart = this.Farts[randomIndex];
    }

    public void Fart()
    {
        SelectRandomFart();
        if (ActiveFart == null) return;

        FartSource.clip = ActiveFart.StartFart;
        FartSource.Play();

        StartTime = Time.time;

        OnFart(UnityEngine.Random.Range(0, 1f));
    }

    public void Update()
    {
        if (ActiveFart == null) return;

        switch (ActiveMode)
        {
            case ACTIVE_MODE.STARTING:
                {
                    if (ActiveFart != null && (Time.time - StartTime) > ActiveFart.StartFart.length)
                    {
                        FartSource.Stop();
                        FartSource.clip = ActiveFart.LoopFart;
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
        FartSource.clip = ActiveFart.EndFart;
        FartSource.loop = false;
        FartSource.Play();
        ActiveMode = ACTIVE_MODE.STARTING;
        ActiveFart = null;
        OnFartStopped(0);
    }
}
