using System.Collections;
using UnityEngine;

public class OnIntroSlidesDone : MonoBehaviour
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
        introManager.OnSlidesDone += StartGame;
    }

    public void StartGame()
    {
        StartCoroutine(LaunchGameLevel());
    }

    private IEnumerator LaunchGameLevel()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.LoadLevel();
    }

    private void OnDisable()
    {
        introManager.OnSlidesDone -= StartGame;
    }
}
