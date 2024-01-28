using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFartMasking : MonoBehaviour
{
    public GameObject Button;
    public LanceScript Lance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FartMask" && Lance.InFlatuenceMode)
        {
            Button.SetActive(true);
        }
    }

    private void OnTr(Collision2D collision)
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Button.active)
        {
            Button.SetActive(false);
        }
    }
}
