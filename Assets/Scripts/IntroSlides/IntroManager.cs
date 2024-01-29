using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public event Action<IIntroSlide> OnLoadSlide = delegate { };
    public event Action<IIntroSlide> OnStopSlide = delegate { };
    public event Action OnSlidesDone = delegate { };
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (slideIndex < slides.Count)
            {
                StopAllCoroutines();
                OnStopSlide(slides[slideIndex]);
            }
        }
    }

    public void ReportSlideDone()
    {
        if (slideIndex < slides.Count)
        {
            StartCoroutine(LaunchNextSlide());
        }
        else
        {
            OnSlidesDone();
        }
    }

    private IEnumerator LaunchNextSlide()
    {
        yield return new WaitForEndOfFrame();
        OnLoadSlide(slides[slideIndex++]);
    }

   
}
