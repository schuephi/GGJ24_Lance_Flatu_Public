using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextEffect : MonoBehaviour
{
    private TextBlendEffect blendEffect;

    private void Start()
    {
        blendEffect = GetComponentInChildren<TextBlendEffect>();
        blendEffect.SetText("Bonjour! My name is Lance Flatu, agent in the name of the empire of Gazovia and his majesty. I will solve all your cases.\nBeware from Lance!");
    }
}
