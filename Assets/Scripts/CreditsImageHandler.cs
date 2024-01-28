using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsImageHandler : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnShowCredits += Instance_OnShowCredits;
        GameManager.Instance.OnHideCredits += Instance_OnHideCredits;
        image.gameObject.SetActive(false);
    }

    private void Instance_OnShowCredits()
    {
        image.gameObject.SetActive(true);
    }

    private void Instance_OnHideCredits()
    {
        image.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnShowCredits -= Instance_OnShowCredits;
        GameManager.Instance.OnHideCredits -= Instance_OnHideCredits;
    }


}
