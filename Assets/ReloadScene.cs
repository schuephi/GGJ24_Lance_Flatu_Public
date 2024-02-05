using TMPro;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    public GameObject TouchText;
    public GameObject DesktopText;

    private GameObject Text;
    public int InputMode = 0;

    public void ReloadGame()
    {
        GameManager.Instance.LoadLevel();
    }

    public void SetInputMode(int mode)
    {
        TouchText.SetActive(false);
        DesktopText.SetActive(false);
        switch(mode)
        {
            case 1:
                Text = TouchText;
                break;
            default:
                Text = DesktopText;
                break;
        }

        Text.SetActive(true);
        InputMode = mode;
    }


    public void OnEnable()
    {
        var blendEffect = Text.GetComponent<TextBlendEffect>();
        var text = blendEffect.GetComponent<TMP_Text>().text;
        blendEffect.SetText(text);
    }

    public void Update()
    {
        if (InputMode == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ReloadGame();
        }
        else if (InputMode == 1 && Input.touchCount > 0)
        {
            ReloadGame();
        }
    }
}
