using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public event Action<IIntroSlide> OnLoadSlide = delegate { };
    private int slideIndex;
    private IList<IIntroSlide> slides;

    private void Awake()
    {
        slides = new List<IIntroSlide>(GetComponentsInChildren<IIntroSlide>(true));
        slideIndex = 0;
    }

    private void Start()
    {
        OnLoadSlide(slides[slideIndex++]);
    }

    public void ReportSlideDone()
    {
        if (slideIndex < slides.Count)
        {
            StartCoroutine(LaunchNextSlide());
        }
        else
        {
            StartCoroutine(LaunchFirstLevel());
        }
    }

    private IEnumerator LaunchNextSlide()
    {
        yield return new WaitForEndOfFrame();
        OnLoadSlide(slides[slideIndex++]);
    }

    private IEnumerator LaunchFirstLevel()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.LoadLevel();
    }
}
