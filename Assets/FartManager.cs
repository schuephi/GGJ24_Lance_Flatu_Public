using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartManager : MonoBehaviour
{
    public event Action<float> OnFart = delegate { };
    public event Action<float> OnFartStopped = delegate { };
    public AudioSource FartSource;

    public List<AudioClip> ShortFarts;
    public List<AudioClip> LongFarts;

    public void Fart()
    {
        var randomIndex = Mathf.Clamp(Mathf.RoundToInt(UnityEngine.Random.Range(0, ShortFarts.Count)), 0, ShortFarts.Count -1);
        FartSource.clip = this.ShortFarts[randomIndex];
        FartSource.Play();
        OnFart(UnityEngine.Random.Range(0, 1f));
    }

    public void StopFart()
    {
        OnFartStopped(0);
    }
}
