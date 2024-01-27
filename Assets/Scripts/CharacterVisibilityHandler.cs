using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisibilityHandler : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private int[] basicValues;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        basicValues = new int[spriteRenderers.Length];
        for (int i = 0; i< spriteRenderers.Length; ++i)
        {
            basicValues[i] = spriteRenderers[i].sortingOrder;
        }
    }
    private void LateUpdate()
    {
        for (int i = 0; i < spriteRenderers.Length; ++i)
        {
            spriteRenderers[i].sortingOrder = basicValues[i] + 1000 - (int)(transform.position.y * 50);
        }
    }
}
