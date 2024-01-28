using System.Collections;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public LanceScript Lance;

    bool wasLanceDead = false;

    // Update is called once per frame
    void Update()
    {
        if (Lance.isDead && wasLanceDead == false)
        {
            StartCoroutine(ShowLooseScreen());
        }

        wasLanceDead = Lance.isDead;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ShowMenu();
        }
    }

    public void HideMenuScreen()
    {
        GameManager.Instance.HideMenu();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    IEnumerator ShowLooseScreen()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ShowLooseScreen();
    }
}
