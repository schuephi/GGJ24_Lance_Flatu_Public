using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatumeter : MonoBehaviour
{
    // Start is called before the first frame update
    public float Flatulence = 0f;
    public RectTransform NeedleTransform;
    public float ShakeAmplitude = 1f;
    public float ShakeSpeed = 1f;

    private LanceScript lance;
    void Start()
    {
        lance = FindFirstObjectByType<LanceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Flatulence= lance.Flatulence;

        switch (this.Flatulence)
        {
            case < 0.2f:
                ShakeAmplitude = 0;
                ShakeSpeed = 0;
                break;
            case < 0.5f:
                ShakeAmplitude = 100;
                ShakeSpeed = 40;
                break;
            case < 0.8f:
                ShakeAmplitude = 500;
                ShakeSpeed = 100;
                break;
            default:
                ShakeAmplitude = 4000;
                ShakeSpeed = 10000;
                break;
        }

        NeedleTransform.rotation = Quaternion.Euler(new Vector3(0, 0, (188 - (Flatulence * 200)) + Mathf.Sin(Time.time * ShakeSpeed) * ShakeAmplitude * Time.deltaTime));
    }
}
