using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public LanceScript Lance;
    public GameObject LooseScreen;

    bool lastLanceDead = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Lance.isDead && lastLanceDead)
        {
            StartCoroutine(ShowLooseScreen());
        }
        lastLanceDead = Lance.isDead;
    }

    IEnumerator ShowLooseScreen()
    {
        yield return new WaitForSeconds(1f);
        LooseScreen.SetActive(true);
    }
}
