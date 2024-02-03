using System.Collections.Generic;
using UnityEngine;

public class ActivateOnScreenControls : MonoBehaviour
{

    public List<GameObject> OnScreenControls;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            foreach (var control in OnScreenControls)
            {
                control.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
