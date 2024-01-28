using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatulenceUIHandler : MonoBehaviour
{
    private StoryIntroManager introManager;
    [SerializeField]
    private GameObject flatumeter;

    private void Awake()
    {
        introManager = FindFirstObjectByType<StoryIntroManager>();
        flatumeter.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        introManager.OnSecondSequenceStart += IntroManager_OnSecondSequenceStart;
    }

    private void IntroManager_OnSecondSequenceStart()
    {
        flatumeter.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        introManager.OnSecondSequenceStart -= IntroManager_OnSecondSequenceStart;
    }
}
