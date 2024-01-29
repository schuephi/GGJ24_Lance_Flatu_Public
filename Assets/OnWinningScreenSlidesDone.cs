using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWinningScreenSlidesDone : MonoBehaviour
{
    [SerializeField]
    private IntroManager introManager;

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        introManager.OnSlidesDone += BackToStart;
    }

    public void BackToStart()
    {
        StartCoroutine(LaunchStartLevel());
    }

    private IEnumerator LaunchStartLevel()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.LoadStartScene();
    }

    private void OnDisable()
    {
        introManager.OnSlidesDone -= BackToStart;
    }
}
