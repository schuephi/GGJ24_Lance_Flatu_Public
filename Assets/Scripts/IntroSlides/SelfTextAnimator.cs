using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text), typeof(TextBlendEffect))]
public class SelfTextAnimator : MonoBehaviour
{
    private TMP_Text text;
    private TextBlendEffect blendEffect;
    private IntroSlideHandler slideHandler;
    private string textToWrite;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        blendEffect = GetComponent<TextBlendEffect>();
        slideHandler = GetComponentInParent<IntroSlideHandler>();
        textToWrite = text.text;
        text.text = "";
    }

    private void OnEnable()
    {
        if (slideHandler is not null)
        {
            slideHandler.OnPrintText += SlideHandler_OnPrintText;
        }
        blendEffect.OnTextTyped += BlendEffect_OnTextTyped;
    }

    private void SlideHandler_OnPrintText()
    {
        blendEffect.SetText(textToWrite);
    }

    private void BlendEffect_OnTextTyped()
    {
        slideHandler.ReportTextPrinted();
    }

    private void OnDisable()
    {
        if (slideHandler is not null)
        {
            slideHandler.OnPrintText -= SlideHandler_OnPrintText;
        }
        blendEffect.OnTextTyped -= BlendEffect_OnTextTyped;
    }
}
