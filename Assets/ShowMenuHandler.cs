using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Image menu;

    private void OnEnable()
    {
        StartCoroutine(DelayRegistration());
    }

    private IEnumerator DelayRegistration()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.OnShowMenu += Instance_OnShowMenu;
        GameManager.Instance.OnHideMenu += Instance_OnHideMenu;
        menu.gameObject.SetActive(false);
    }

    private void Instance_OnShowMenu()
    {
        menu.gameObject.SetActive(true);
    }

    private void Instance_OnHideMenu()
    {
        menu.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnShowMenu -= Instance_OnShowMenu;
        GameManager.Instance.OnHideMenu -= Instance_OnHideMenu;
    }
}
