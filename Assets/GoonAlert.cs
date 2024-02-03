using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonAlert : MonoBehaviour
{
    public GameObject FillRect;
    private float PosStart = 0f;
    // Start is called before the first frame update

    public void Start()
    {
        PosStart = FillRect.transform.localPosition.y;
    }

    public void SetFillLevel(float level)
    {
        FillRect.transform.localPosition = new Vector3(0, PosStart + Mathf.Lerp(0, 1, Mathf.Clamp01(level)), 0);
    }
}
