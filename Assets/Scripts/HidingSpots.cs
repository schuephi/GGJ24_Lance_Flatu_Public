using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HidingSpots : MonoBehaviour
{
    public UnityEvent OnPlayerHide;
    public UnityEvent OnPlayerUnhide;
    public UnityEvent OnPlayerFailedInteraction;

    private bool isPlayerInside = false;
    private bool isPlayerInReach = false;
    [SerializeField]
    private UnityEngine.UI.Image keyIndicator;

    private LanceScript lanceScript;

    private void Start()
    {
        lanceScript = GameObject.FindGameObjectWithTag("Lance").GetComponent<LanceScript>();
        keyIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInReach && Input.GetKeyDown(KeyCode.E))
        {
            if (!lanceScript.IsImmobile) {
            OnPlayerHide?.Invoke();
            isPlayerInside = true;

            Debug.Log("Player tries to enter hiding spot");
            }
            else
            {
                OnPlayerFailedInteraction?.Invoke();
            }
        }

        if (isPlayerInside && Input.GetKeyUp(KeyCode.E))
        {
            OnPlayerUnhide?.Invoke();
            isPlayerInside = false;
            Debug.Log("Player leaves hiding spot");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = true;
            keyIndicator.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = false;
            keyIndicator.gameObject.SetActive(false);
        }
    }

}
