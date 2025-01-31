using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntroAudioHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private IntroSlideHandler slideHandler;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        slideHandler = GetComponentInParent<IntroSlideHandler>();
    }

    private void OnEnable()
    {
        if (slideHandler is not null)
        {
            slideHandler.OnPrintText += SlideHandler_OnPrintText;
            slideHandler.OnStop += SlideHandler_OnStop;
        }
    }

    private void SlideHandler_OnPrintText()
    {
        audioSource.Play();
    }

    private void SlideHandler_OnStop()
    {
        audioSource.Stop();
    }

    private void OnDisable()
    {
        if (slideHandler is not null)
        {
            slideHandler.OnPrintText -= SlideHandler_OnPrintText;
            slideHandler.OnStop -= SlideHandler_OnStop;
        }
    }
}
