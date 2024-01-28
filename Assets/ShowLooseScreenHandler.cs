using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLooseScreenHandler : MonoBehaviour
{
    [SerializeField]
    private Image looseScreen;

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnShowLooseScreen += Instance_OnShowLooseScreen;
        GameManager.Instance.OnHideLooseScreen += Instance_OnHideLooseScreen;
        looseScreen.gameObject.SetActive(false);
    }

    private void Instance_OnShowLooseScreen()
    {
        looseScreen.gameObject.SetActive(true);
    }

    private void Instance_OnHideLooseScreen()
    {
        looseScreen.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnShowLooseScreen -= Instance_OnShowLooseScreen;
        GameManager.Instance.OnHideLooseScreen -= Instance_OnHideLooseScreen;
    }
}
