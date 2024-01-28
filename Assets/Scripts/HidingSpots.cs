using System;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpots : MonoBehaviour
{
    public UnityEvent OnPlayerHide;
    public UnityEvent OnPlayerUnhide;
    public UnityEvent OnPlayerFailedInteraction;

    private bool isPlayerInside = false;
    private bool isPlayerInReach = false;

    private LanceScript lanceScript;

    private void Start()
    {
        lanceScript = GameObject.FindGameObjectWithTag("Lance").GetComponent<LanceScript>();
    }

    private void Update()
    {
        if (isPlayerInReach && Input.GetKeyDown(KeyCode.E))
        {
            if (lanceScript.IsImmobile) {
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
            if (lanceScript.IsImmobile) {
                OnPlayerUnhide?.Invoke();
                isPlayerInside = false;
                Debug.Log("Player leaves hiding spot");
            }
            else
            {
                OnPlayerFailedInteraction?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            isPlayerInReach = false;
        }
    }

}
