using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VisibilityCovrageHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int startLayer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startLayer = spriteRenderer.sortingOrder;
        spriteRenderer.sortingOrder = (int)(startLayer + 1000 - (transform.position.y - transform.localPosition.y) * 50);
    }
}
