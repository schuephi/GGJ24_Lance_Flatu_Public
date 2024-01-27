using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class IntroSlideHandler : MonoBehaviour, IIntroSlide
{
    public event Action OnPrintText = delegate { };
    [SerializeField]
    private bool useFadeEffect = true;
    [SerializeField]
    private float fadeInTime = 0.2f;
    [SerializeField]
    private float fadeOutTime = 0.2f;
    [SerializeField]
    private float slideTransitionTime = 0.2f;

    private IntroManager introManager;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        introManager = GetComponentInParent<IntroManager>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        if (introManager is not null)
        {
            introManager.OnLoadSlide += IntroManager_OnLoadSlide;
        }
    }

    public void ReportTextPrinted()
    {
        StartCoroutine(DelaySlideSwitch());
    }

    private IEnumerator DelaySlideSwitch()
    {
        yield return new WaitForSeconds(slideTransitionTime);
        if (useFadeEffect)
        {
            StartCoroutine(DoFadeOut(fadeOutTime));
        }
        else
        {
            introManager.ReportSlideDone();
        }
    }

    private void IntroManager_OnLoadSlide(IIntroSlide slide)
    {
        if (slide.Equals(this))
        {
            if (useFadeEffect)
            {
                StartCoroutine(DoFadeIn(fadeInTime));
            }
            else
            {
                canvasGroup.alpha = 1;
                OnPrintText();
            }
        }
    }

    private IEnumerator DoFadeOut(float time = 1f)
    {
        var elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp(1 - elapsedTime / time, 0, 1f);
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = 0f;
        yield return new WaitForEndOfFrame();
        introManager.ReportSlideDone();
    }

    private IEnumerator DoFadeIn(float time = 1f)
    {
        var elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp(elapsedTime / time, 0, 1f);
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = 1f;
        OnPrintText();
    }

    private void OnDisable()
    {
        if (introManager is not null)
        {
            introManager.OnLoadSlide -= IntroManager_OnLoadSlide;
        }
    }
}
