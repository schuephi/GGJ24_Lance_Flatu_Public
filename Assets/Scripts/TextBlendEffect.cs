using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextBlendEffect : MonoBehaviour
{
    public event Action OnCharacterTyped = delegate { };
    public event Action OnTextTyped = delegate { };
    private TMP_Text textToAnimate;
    [SerializeField]
    private float typeEffectSpeed = 0.1f;
    private bool isTyping;

    public void SetText(string text)
    {
        if (!isTyping)
        {
            StartCoroutine(TypeWriterEffect(text));
        }
    }

    private IEnumerator TypeWriterEffect(string fullText)
    {
        textToAnimate = GetComponent<TMP_Text>();
        isTyping = true;
        textToAnimate.text = "";
        var currentIndex = 0;
        while (!textToAnimate.text.Equals(fullText))
        {
            textToAnimate.text += fullText.Substring(currentIndex++, 1);
            OnCharacterTyped();
            yield return new WaitForSeconds(typeEffectSpeed);
        }
        isTyping = false;
        OnTextTyped();
    }
}
