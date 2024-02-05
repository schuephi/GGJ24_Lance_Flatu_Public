using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActivateOnScreenControls : MonoBehaviour
{
    public ReloadScene ReloadScene;
    public List<GameObject> OnScreenControls;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform || Application.isEditor)
        {
            foreach (var control in OnScreenControls)
            {
                control.SetActive(true);
            }

            ReloadScene.SetInputMode(1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
