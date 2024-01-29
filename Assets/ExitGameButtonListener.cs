using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButtonListener : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        GameManager.Instance.QuitGame();
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
