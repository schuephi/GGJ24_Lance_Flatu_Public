using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloadGame()
    {
        GameManager.Instance.LoadLevel();
    }

    public void OnEnable()
    {
        var blendEffect = GetComponentInChildren<TextBlendEffect>();
        var text = blendEffect.GetComponent<TMP_Text>().text;
        blendEffect.SetText(text);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            ReloadGame();
        }
    }
}
