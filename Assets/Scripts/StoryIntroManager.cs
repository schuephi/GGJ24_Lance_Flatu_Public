using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoryIntroManager : MonoBehaviour
{
    public event Action OnSecondSequenceStart = delegate { };

    [SerializeField]
    private AudioClip IntroClip1;
    [SerializeField]
    private AudioClip IntroClip2;

    private AudioSource audioSource;
    private LanceScript lanceScript;

    public bool FartingPhaseStarted { get; private set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lanceScript = FindAnyObjectByType<LanceScript>();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnFlatuencesStart += Instance_OnFlatuencesStart;
        audioSource.clip = IntroClip1;
        audioSource.Play();
    }

    private void Instance_OnFlatuencesStart()
    {
        FartingPhaseStarted = true;
        audioSource.clip = IntroClip2;
        audioSource.Play();
        StartCoroutine(DelayFart());
    }

    private IEnumerator DelayFart()
    {
        yield return new WaitForSeconds(15f);
        lanceScript.InFlatuenceMode = true;
        lanceScript.FartManager.Fart(0.4f);
        StartCoroutine(DelayFartStop());
    }

    private IEnumerator DelayFartStop()
    {
        yield return new WaitForEndOfFrame();
        lanceScript.FartManager.StopFart();
        OnSecondSequenceStart();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnFlatuencesStart -= Instance_OnFlatuencesStart;
    }
}
